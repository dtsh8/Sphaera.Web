import React from 'react';
import { Link } from 'react-router-dom';
import { Grid } from 'react-bootstrap';
import { Form } from 'devextreme-react';
import CustomStore from 'devextreme/data/custom_store';
import ruMessages from 'devextreme/localization/messages/ru.json';
import { locale, loadMessages } from 'devextreme/localization';
import L from 'leaflet';
import icon from 'leaflet/dist/images/marker-icon.png';
import iconShadow from 'leaflet/dist/images/marker-shadow.png';
import { find, get, merge } from 'lodash';
import { DropzoneComponent } from '../Dropzone';
import bem from '../../lib/bem';
import './style.css';
import ButtonComponent from '../controls/Button';
import { Map } from './Map';
import { initIncidentTypes } from '../../helpers/dictionaries';
import { ROUTE_APPEALS } from '../../constants/routes';
import context from '../../api/api';

const DefaultIcon = L.icon({
  iconUrl: icon,
  shadowUrl: iconShadow
});

L.Marker.prototype.options.icon = DefaultIcon;

loadMessages(ruMessages);
locale('ru-RU');

const block = 'requestForm';

export class RequestPage extends React.PureComponent {
  constructor(props) {
    super(props);

    this.coordinates = props.userLocation;

    this.state = {
      formDataValues: {
        ...this.initForm()
      },
      files: []
    };
  }

  onSubmit = (e) => {
    const { history, address } = this.props;
    const {
      formDataValues: {
        typeOfIncident1,
        typeOfIncident2,
        typeOfIncident3,
        notes,
        municipalityAlias,
        localityFias,
        locality,
        streetFias,
        street,
        house,
        building,
        entrance,
        floor,
        apartment,
        highwayName,
        highwayKm
      },
      files
    } = this.state;

    const typeCode = typeOfIncident3 || typeOfIncident2 || typeOfIncident1;
    const typeName = this.props.types.results.find(item => item.code === typeCode).indexName;
    const filePromises = files.map((file) => {
      return new Promise((resolve) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = (res) => {
          const splitSeparator = 'base64,';
          const base64FileBody = res.target.result.split(splitSeparator)[1];
          resolve({
            fileName: file.name,
            fileBody: base64FileBody
          });
        };
      });
    });

    Promise.all(filePromises).then((formattedFiles) => {
      this.props.onSubmitForm({
        typeCode,
        typeName,
        notes,
        files: formattedFiles,
        history,
        address: {
          localityFias,
          locality: locality && locality.localityName,
          streetFias,
          street: street && street.streetName,
          municipalityAlias,
          house,
          building,
          entrance,
          floor,
          apartment,
          highway: {
            name: highwayName,
            km: highwayKm
          }
        }
      });
    });

    e.preventDefault();
  };

  onCoordsChanged = (data) => {
    this.coordinates = {
      coords: data
    };
  };

  onFilesChanged = (data) => {
    this.setState({
      files: data
    });
  };

  onSelectItemClick = id => (e) => {
    const {
      formDataValues: {
        typeOfIncident1,
        typeOfIncident2,
        typeOfIncident3
      }
    } = this.state;
    const formDataValues = { ...this.state.formDataValues };
    const typesOfIncident = [typeOfIncident1, typeOfIncident2, typeOfIncident3];

    typesOfIncident.forEach((item, index) => {
      if (index + 1 >= Number(id)) {
        formDataValues[`typeOfIncident${index + 1}`] = null;
      }

      formDataValues[`typeOfIncident${id}`] = e.itemData.code;
    });

    this.setState({
      formDataValues
    });
  };

  getAddressFormItems = () => {
    const {
      formDataValues: {
        municipality,
        locality,
        street
      },
    } = this.state;

    const defaultLookup = {
      editorType: 'dxLookup',
      colSpan: 12,
      editorOptions: {
        showCancelButton: false,
        showPopupTitle: false,
        popupHeight: 'auto',
        closeOnOutsideClick: true,
        searchTimeout: 100,
      },
      label: {
        location: 'top'
      }
    };
    
    const municipalityItem = merge({}, defaultLookup, {
      dataField: 'municipalityFias',
      editorOptions: {
        placeholder: 'Выберите муниципальное образование',
        dataSource: new CustomStore({ key: 'fiasCode', load: () => context.municipality.getList(), byKey: key => (municipality && municipality.fiasCode === key ? municipality : null) }),
        displayExpr: 'name',
        valueExpr: 'fiasCode',
        onItemClick: (e) => {
          this.setState(prevState => ({ 
            ...prevState, 
            formDataValues: { 
              ...prevState.formDataValues,
              municipality: e.itemData,
              municipalityFias: e.itemData.fiasCode,
              municipalityAlias: e.itemData.alias,
              locality: null,
              street: null 
            }
          }));
        },
        value: municipality && municipality.fiasCode
      },
      label: {
        text: 'Муниципальное образование',
      },
      validationRules: [{
        type: 'required',
        message: 'Это обязательное поле'
      }]
    });
    const localityItem = merge({}, defaultLookup, {
      dataField: 'localityFias',
      editorOptions: {
        placeholder: 'Выберите населенный пункт',
        dataSource: new CustomStore({ 
          key: 'localityFiasCode', 
          load: p => context.address.findLocality(municipality && municipality.fiasCode, p.searchValue),
          byKey: key => (locality && locality.localityFiasCode === key ? locality : null)
        }),
        displayExpr: 'localitySuggestion',
        valueExpr: 'localityFiasCode',
        onItemClick: e => this.setState(prevState => ({ 
          ...prevState,
          formDataValues: { 
            ...prevState.formDataValues,
            locality: e.itemData,
            localityFias: e.itemData.localityFiasCode,
            street: null
          } 
        })),
        value: locality && locality.localityFiasCode
      },
      disabled: !municipality,
      label: {
        text: 'Населенный пункт',
      }
    });
    const streetItem = merge({}, defaultLookup, {
      dataField: 'streetFias',
      editorOptions: {
        placeholder: 'Выберите улицу',
        dataSource: new CustomStore({ 
          key: 'streetFiasCode',
          load: p => context.address.findStreet(municipality && municipality.fiasCode, locality || {}, p.searchValue),
          byKey: key => (street && street.streetFiasCode === key ? street : null)
        }),
        displayExpr: 'streetSuggestion',
        valueExpr: 'streetFiasCode',
        onItemClick: e => this.setState(prevState => ({ 
          ...prevState,
          formDataValues: { 
            ...prevState.formDataValues, 
            street: e.itemData,
            streetFias: e.itemData.streetFiasCode
          } 
        })),
        value: street && street.streetFiasCode
      },
      disabled: !municipality,
      label: {
        text: 'Улица',
      }
    });
    const houseItem = {
      editorType: 'dxTextBox',
      dataField: 'house',
      colSpan: 8,
      cssClass: 'house',
      editorOptions: {
        placeholder: 'Номер дома',
      },
      label: {
        text: 'Дом',
        location: 'top'
      }
    };
    const buildingItem = {
      editorType: 'dxTextBox',
      dataField: 'building',
      colSpan: 4,
      cssClass: 'building',
      editorOptions: {
        placeholder: 'Корпус'
      },
      label: {
        text: 'Корпус',
        location: 'top'
      }
    };
    const entranceItem = {
      editorType: 'dxTextBox',
      dataField: 'entrance',
      colSpan: 4,
      cssClass: 'entrance',
      editorOptions: {
        placeholder: 'Подъезд'
      },
      label: {
        text: 'Подъезд',
        location: 'top'
      }
    };
    const floorItem = {
      editorType: 'dxTextBox',
      dataField: 'floor',
      colSpan: 4,
      cssClass: 'floor',
      editorOptions: {
        placeholder: 'Этаж'
      },
      label: {
        text: 'Этаж',
        location: 'top'
      }
    };
    const apartmentItem = {
      editorType: 'dxTextBox',
      dataField: 'apartment',
      colSpan: 4,
      cssClass: 'apartment',
      editorOptions: {
        placeholder: 'Квартира'
      },
      label: {
        text: 'Квартира',
        location: 'top'
      }
    };
    const highwayNameItem = {
      editorType: 'dxTextBox',
      dataField: 'highwayName',
      colSpan: 6,
      cssClass: 'highway-name',
      editorOptions: {
        placeholder: 'Шоссе (трасса)'
      },
      label: {
        text: 'Шоссе (трасса)',
        location: 'top'
      }
    };
    const highwayKmItem = {
      editorType: 'dxTextBox',
      dataField: 'highwayKm',
      colSpan: 6,
      cssClass: 'highway-km',
      editorOptions: {
        placeholder: 'Километр'
      },
      label: {
        text: 'Километр',
        location: 'top'
      }
    };
    return [
      municipalityItem,
      localityItem,
      streetItem,
      houseItem,
      buildingItem,
      entranceItem,
      floorItem,
      apartmentItem,
      highwayNameItem,
      highwayKmItem
    ];
  }

  getFormItems = () => {
    return [
      {
        itemType: 'group',
        caption: '',
        items: this.editIndexFormItems()
      },
      {
        itemType: 'group',
        caption: 'Место события:',
        items: this.getAddressFormItems(),
        colCount: 12
      },
      {
        itemType: 'group',
        caption: 'Сопроводительный текст:',
        cssClass: 'np',
        items: [{
          dataField: 'notes',
          label: {
            location: 'top',
            text: '',
            visible: false
          },
          editorType: 'dxTextArea',
          editorOptions: {
            height: 83
          },
          validationRules: [{
            type: 'required',
            message: 'Это обязательное поле'
          }, {
            type: 'stringLength',
            max: 300,
            message: 'Не более 300 символов'
          }]
        }]
      }
    ];
  };

  initForm = () => {
    return {
      typeOfIncident1: null,
      typeOfIncident2: null,
      typeOfIncident3: null,
      notes: ''
    };
  };

  editIndexFormItems = () => {
    const {
      formDataValues
    } = this.state;
    const selectTypes = this.props.types.results;

    const firstLevelTypes = initIncidentTypes(selectTypes);
    const dataSource = [firstLevelTypes];

    return dataSource.map((data, index) => {
      const level = index + 1;
      const validation = level === 1 ?
        {
          validationRules: [{
            type: 'required',
            message: 'Это обязательное поле'
          }]
        } :
        {};

      return {
        dataField: `typeOfIncident${level}`,
        editorType: 'dxLookup',
        editorOptions: {
          placeholder: 'Выберите тип происшествия',
          dataSource: data,
          displayExpr: 'indexName',
          valueExpr: 'code',
          showCancelButton: false,
          showPopupTitle: false,
          popupHeight: 'auto',
          closeOnOutsideClick: true,
          searchTimeout: 100,
          onItemClick: this.onSelectItemClick(level),
          value: formDataValues[`typesOfIncident${level}`],
        },
        disabled: level === 1 ? false : !formDataValues[`typeOfIncident${level - 1}`],
        label: {
          text: 'Тип происшествия',
          location: 'top'
        },
        ...validation
      };
    });
  };

  formRef = (node) => {
    return this.form = node ? node.instance : null;
  };

  dropzoneRef = node => this.dropzone = node;

  render() {
    const {
      isAppealSending,
    } = this.props;
    const { formDataValues } = this.state;

    return (
      <Grid fluid>
        <Grid className={bem({ block })}>
          <form onSubmit={this.onSubmit}>
            <Form
              ref={this.formRef}
              validationGroup={block}
              formData={formDataValues}
              colCount="auto"
              colCountByScreen={{
                md: 1,
                sm: 1
              }}
              screenByWidth={(width) => {
                return width < 720 ? 'sm' : 'md';
              }}
              showRequiredMark={false}
              items={this.getFormItems()}
            />
            <p className="dx-form-group-caption">Приложенные файлы</p>
            <DropzoneComponent
              ref={this.dropzoneRef}
              wrapperClassName={bem({ block, elem: 'dropzone-wrapper' })}
              className={bem({ block, elem: 'dropzone' })}
              activeClassName={bem({ block, elem: 'dropzone-active' })}
              maxSize={25 * 1024 * 1024}
              maxCount={4}
              onFilesChanged={this.onFilesChanged}
            >
              <p className={bem({ block, elem: 'dropzone-head' })}>Перетащите файлы сюда</p>
              <p className={bem({ block, elem: 'dropzone-attention' })}>Загрузить можно не больше 4х файлов общим размером 25мб</p>
            </DropzoneComponent>
            <ButtonComponent
              height={50}
              text="Создать обращение"
              type="success"
              className={bem({ block, elem: 'button-submit' })}
              validationGroup={block}
              disabled={isAppealSending}
              useSubmitBehavior
            />
            <Link to={ROUTE_APPEALS}>
              <ButtonComponent
                height={50}
                text="Отменить создание обращения"
                type="normal"
                className={bem({ block, elem: 'button-cancel' })}
                validationGroup={block}
              />
            </Link>
          </form>
        </Grid>
      </Grid>
    );
  }
}
