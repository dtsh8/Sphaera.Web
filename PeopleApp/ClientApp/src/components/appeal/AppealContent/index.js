import React from 'react';
import { Grid, Col, Row } from 'react-bootstrap';
import { Map, Marker, WMSTileLayer, ZoomControl } from 'react-leaflet';
import moment from 'moment';
import context from '../../../api/api';
import bem from '../../../lib/bem';
import './style.css';

const block = 'appeal';

const doneStatus = 'Реагирование завершено';
const newStatus = 'Новое';
const workStatus = 'В работе';
const rejectedStatus = 'Отклонено';
const zoom = 17;

export class Appeal extends React.PureComponent {
  constructor(props) {
    super(props);
    this.state = {
      gisServerUri: '',
      gisServerLayers: ''
    };
    context.settings.getGisServerSettings()
    .then((settings) => 
      this.setState({gisServerUri: settings.Uri, gisServerLayers: settings.Layers})
    );
  }
  getStatusMods = () => {
    const { stateName } = this.props.appeal;

    return stateName ? {
      done: stateName === doneStatus,
      new: stateName === newStatus,
      'at-work': stateName === workStatus,
      rejected: stateName === rejectedStatus
    } : {};
  };

  downloadFile = (fileName) => {
    const { downloadFile } = this.props;
    downloadFile(fileName);
  };

  downloadAllFiles = () => {
    const { appeal: { files }, downloadAllFiles } = this.props;
    const fileNames = files.map(file => file.FileName);
    downloadAllFiles(fileNames);
  };

  render() {
    const {
      missionId,
      created,
      stateName,
      message,
      latitude,
      longitude,
      files,
      address,
    } = this.props.appeal;

    return (
      <div className={bem({ block })}>
        <Grid>
          <Row>
            <Col xs={12}>
              <div className={bem({ block, elem: 'main-inf-wrapper' })}>
                <ul className={bem({ block, elem: 'main-inf' })}>
                  <li className={bem({ block, elem: 'main-inf-block' })}>
                    <div className={bem({ block, elem: 'main-inf-title' })}>Номер обращения</div>
                    <div className={bem({ block, elem: 'main-inf-content' })}>{missionId || ''}</div>
                  </li>
                  <li className={bem({ block, elem: 'main-inf-block' })}>
                    <div className={bem({ block, elem: 'main-inf-title' })}>Зарегистрировано</div>
                    <div className={bem({ block, elem: 'main-inf-content' })}>{moment(created).format('DD.MM.YYYY hh:mm') || ''}</div>
                  </li>
                  <li className={bem({ block, elem: 'main-inf-block' })}>
                    <div className={bem({ block, elem: 'main-inf-title' })}>Статус</div>
                    <div className={bem({ block, elem: 'main-inf-content status', mods: this.getStatusMods() })}>{stateName || ''}</div>
                  </li>
                </ul>
              </div>

              <div className={bem({ block, elem: 'comment' })}>
                <div className={bem({ block, elem: 'comment-title' })}>Сопроводительный текст</div>
                <div className={bem({ block, elem: 'comment-content' })}>{message}</div>
              </div>

              {latitude && longitude ?
                <div className={bem({ block, elem: 'map-block' })}>
                  <div className={bem({ block, elem: 'map-title' })}>Место события:</div>
                  <div className={bem({ block, elem: 'map-wrapper' })}>
                    <Map
                      center={[latitude, longitude]}
                      zoom={zoom}
                      zoomControl={false}
                      scrollWheelZoom={false}
                    >
                      <WMSTileLayer
                        url={this.state.gisServerUri}
                        layers={this.state.gisServerLayers}
                      />
                      <ZoomControl
                        zoomInTitle="Увеличить"
                        position="topright"
                        zoomOutTitle="Уменьшить"
                      />
                      <Marker position={[latitude, longitude]} />
                    </Map>
                  </div>
                </div> :
                ''
              }

              <div className={bem({ block, elem: 'address-block' })}>
                <div className={bem({ block, elem: 'address-title' })}>Место события:</div>
                <div className={bem({ block, elem: 'address-content' })}>{address}</div>
              </div>

              <div className={bem({ block, elem: 'files' })}>
                <div className={bem({ block, elem: 'files-header' })}>
                  <div className={bem({ block, elem: 'files-title' })}>Приложенные файлы</div>
                  <div className={bem({ block, elem: 'files-download' })} onClick={this.downloadAllFiles}>
                    <span className="icon icon-download-2" />
                    Скачать файлы
                  </div>
                  <div className="clearfix" />
                </div>
                {files && !!files.length ?
                  <table className={bem({ block, elem: 'files-table' })}>
                    <tbody>
                      {files.map(file => (
                        <tr className={bem({ block, elem: 'files-table-row' })} key={file.FileName}>
                          <td>
                            <span className="icon-file" />
                          </td>
                          <td>
                            {file.FileName}
                          </td>
                          <td>{file.description}</td>
                          <td className={bem({ block, elem: 'actions-cell' })}>
                            {/* <span className="icon-printer" /> */}
                            <span className="icon-download-3" onClick={() => this.downloadFile(file.FileName)} />
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table> :
                  ''
                }
              </div>
            </Col>
          </Row>
        </Grid>
      </div>
    );
  }
}
