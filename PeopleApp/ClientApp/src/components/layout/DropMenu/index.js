import React, { Component } from 'react';

/**
 * It's the abstract class to display the dropdown menu
 * For using this class you should inherit from it and override render
 * also add to button: ref={(c) => { this.dropdownButton = c; }}
 * and add to dropdown menu: ref={(c) => { this.dropdown = c; }}
 */
export class DropMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
        show: false, //eslint-disable-line
    };
  }

  componentWillUnmount() {
    window.removeEventListener('click', this.handleDocumentClick);
  }

  dropdown = null;
  dropdownButton = null;

  handleDocumentClick = (e) => {
    const area = this.dropdown || {};
    const areaButton = this.dropdownButton || {};
    const node = e.target || null;
    if (area.contains && !area.contains(node) && !areaButton.contains(node)) {
      this.closeDropMenu();
    }
  };

  showDropMenu = () => {
    this.setState(prev => ({
      show: !prev.show,
    }), () => {
      window.addEventListener('click', this.handleDocumentClick);
    });
  };

  closeDropMenu = () => {
    this.setState({ show: false }, () => { //eslint-disable-line
      window.removeEventListener('click', this.handleDocumentClick);
    });
  };

  render() {
    return <div />;
  }
}
