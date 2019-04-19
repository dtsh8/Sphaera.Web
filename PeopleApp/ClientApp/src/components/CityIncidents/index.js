import React from 'react';
import bem from '../../lib/bem';
import { getDictionaryList } from '../../helpers/dictionaries';
import { Divider } from '../Divider';
import FormControl from '../FormControl';
import Lookup from '../controls/LookUp';
import Button from '../controls/Button';
import './style.css';

const block = 'city-incidents';

const defaultValues = {
  type: { id: -1, name: 'Все происшествия' }
};

export default class CityIncidents extends React.PureComponent {
  state = {
    type: defaultValues.type,
    filterShowIncident: true
  };

  componentDidMount() {
    this.getCurrentFilters();
  }

  onFilterChanged = (val, key, dict) => {
    const { type } = this.state;
    const value = dict.results.find(d => d.name === val.value) || defaultValues[key];
    const newState = { type };
    newState[key] = value;
    this.setState(newState);
  };

  getCurrentFilters = () => {
    const { filters: { type } } = this.props;
    this.setState({ type });
  };

  getCityIncidentsMods = () => {
    return {
      'max-content': !!this.props.cityIncidentMaxContent
    };
  };

  handleMobileFilterClick = () => {
    this.setState(prevState => ({
      filterShowIncident: !prevState.filterShowIncident
    }));
  };

  render() {
    const {
      types,
      onChangeIncidentsFilter
    } = this.props;

    const {
      type,
      filterShowIncident
    } = this.state;

    return (
      <div className={bem({ block, elem: 'wrapper', mods: this.getCityIncidentsMods() })}>
        <div
          className={bem({ block, elem: 'mobile-filter' })}
          onClick={this.handleMobileFilterClick}
        >
          <span className="icon-filter" />

          <span className={bem({ block, elem: 'mobile-filter-title' })}>
            Фильтр
          </span>

        </div>

        {filterShowIncident && (
          <div className={bem({ block })}>
            <span className={bem({ block, elem: 'title' })}>
            Текущие происшествия
            </span>

            <Divider />
            <div className={bem({ block, elem: 'margin' })} />

            <FormControl label="Выберите тип происшествия">
              <Lookup
                dataSource={getDictionaryList(types, defaultValues.type)}
                value={type.name}
                placeholder="Все происшествия"
                height={40}
                showCancelButton={false}
                showPopupTitle={false}
                popupHeight="auto"
                closeOnOutsideClick
                searchTimeout={100}
                onValueChanged={val => this.onFilterChanged(val, 'type', types)}
              />
            </FormControl>

            <Button
              text="Показать"
              height={40}
              className={bem({ block, elem: 'button' })}
              onClick={() => onChangeIncidentsFilter({ type })}
            />

          </div>
        )}
      </div>
    );
  }
}
