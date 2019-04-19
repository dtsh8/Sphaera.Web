import React, { Fragment } from 'react';
import Dropzone from 'react-dropzone';

export class DropzoneComponent extends React.PureComponent {
  static defaultProps = {
    wrapperClassName: 'dropzone-wrapper',
    className: 'dropzone',
    activeClassName: 'dropzone-active',
    acceptClassName: 'dropzone-accept',
    maxSize: 10 * 1024,
    maxCount: 10,
    accept: '' // все файлы
  };

  state = {
    accepted: [],
    rejected: [],
    error: ''
  };

  // проверка файлов
  // файлы большего размера до сюда не доходят
  onDrop = (accepted, rejected) => {
    let acceptedsize = 0;
    let error = '';

    // предыдущие файлы не исчезают при повторном добавлении
    const tempAccepted = [...this.state.accepted, ...accepted];
    let tempRejected = [...this.state.rejected, ...rejected];

    tempAccepted.forEach((file, i) => {
      if (acceptedsize + file.size > this.props.maxSize) {
        error = 'Размер файлов превышает допустимое ограничение';
        tempRejected.push(tempAccepted.slice(i, i + 1)[0]);
        tempAccepted.splice(i, 1);
        i -= 1;
      } else {
        acceptedsize += file.size;
      }

      if (i + 1 > this.props.maxCount) {
        error = 'Количество файлов превышает допустимое ограничение';
        tempRejected = [...tempRejected, ...tempAccepted.splice(i, tempAccepted.length - i)];
      }
    });

    this.setState({
      accepted: tempAccepted,
      rejected: tempRejected,
      error
    });

    this.props.onFilesChanged(tempAccepted);
  };

  // очистка локально
  clearDropzone = () => {
    this.setState({
      accepted: [],
      rejected: [],
      error: ''
    });
  };

  deleteFileFromAccepted = (fileName) => {
    const acceptedFiles = [...this.state.accepted];
    let fileIndex = null;
    this.state.accepted.forEach((file, index) => {
      if (file.name === fileName) {
        fileIndex = index;
      }
    });
    acceptedFiles.splice(fileIndex, 1);
    this.setState(
      { accepted: [...acceptedFiles] },
      () => {
        const { accepted } = this.state;
        this.props.onFilesChanged(accepted);
      }
    );
  };

  deleteFileFromRejected = (fileName) => {
    const rejectedFiles = [...this.state.rejected];
    let fileIndex = null;
    this.state.rejected.forEach((file, index) => {
      if (file.name === fileName) {
        fileIndex = index;
      }
    });
    rejectedFiles.splice(fileIndex, 1);
    this.setState({ rejected: [...rejectedFiles] });
  };

  render() {
    const {
      className,
      activeClassName,
      acceptClassName,
      maxSize,
      children,
      accept,
      wrapperClassName,
    } = this.props;

    return (
      <div className={wrapperClassName}>
        {
          this.state.accepted.length ?
            <Fragment>
              <p className="title">Принятые:</p>
              <table className="files-table">
                <tbody>
                  {this.state.accepted.map(file => (
                    <tr key={file.name} className="files-table-row">
                      <td className="icon-cell">
                        <span className="icon-file" />
                      </td>
                      <td className="name-cell">{file.name}</td>
                      <td className="size-cell">{file.size} байт</td>
                      <td className="action-cell">
                        <span className="icon icon-delete" onClick={() => this.deleteFileFromAccepted(file.name)} />
                      </td>
                    </tr>)
                  )}
                </tbody>
              </table>
            </Fragment>
            : ''
        }
        {
          this.state.rejected.length ?
            <Fragment>
              <p className="title-rejected">Отклоненные:</p>
              <table className="files-table">
                <tbody>
                  {this.state.rejected.map(file => (
                    <tr key={file.name} className="files-table-row">
                      <td className="icon-cell">
                        <span className="icon-file" />
                      </td>
                      <td className="name-cell">{file.name}</td>
                      <td className="size-cell">{file.size} байт</td>
                      <td className="action-cell">
                        <span className="icon icon-delete" onClick={() => this.deleteFileFromRejected(file.name)} />
                      </td>
                    </tr>)
                  )}
                </tbody>
              </table>
            </Fragment>
            : ''
        }
        <Dropzone
          className={className}
          activeClassName={activeClassName}
          acceptClassName={acceptClassName}
          onDrop={this.onDrop}
          maxSize={maxSize}
          accept={accept}
        >
          {children}
          {
            this.state.error ?
              <p className="errors">
                {this.state.error}
              </p>
              : ''
          }
        </Dropzone>
      </div>
    );
  }
}
