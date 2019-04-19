import React from 'react';
import { TreeView } from 'devextreme-react';
import { Glyphicon } from 'react-bootstrap';
import { TREE_ITEM_TYPE_FOLDER, TREE_ITEM_TYPE_DOCUMENT } from './../../../helpers/reminders';
import bem from './../../../lib/bem';
import './style.css';

const block = 'reminders-menu';

export class RemindersMenu extends React.PureComponent {

  itemClick = (item) => {
    const {
      changeActiveReminder
    } = this.props;
    const {
      itemData: {
        reminder
      }
    } = item;
    
    if (reminder.DocumentTreeItemType === TREE_ITEM_TYPE_DOCUMENT) {
      changeActiveReminder(reminder.Id);
    } else if (item.node.expanded) {
      item.component.collapseItem(item.itemElement);
    } else {
      item.component.expandItem(item.itemElement);
    }
  }

  renderItem = (item) => {
    let className = bem({ block, elem: 'tree-item' });

    const isActive = item.reminder.Id === this.props.activeAccordionItemId;
    if (isActive) {
      className += ' active';
    }

    if (item.reminder.DocumentTreeItemType === TREE_ITEM_TYPE_FOLDER) {
      return (
        <h4 className={className}>
          {item.reminder.DocumentTreeItemType === TREE_ITEM_TYPE_FOLDER && <Glyphicon className={bem({ block, elem: 'folder' })} glyph="folder-open" />}
          {item.reminder.Name}
        </h4>
      );
    }

    return (
      <span className={className}>
        {isActive && <Glyphicon className={bem({ block, elem: 'arrow-right' })} glyph="arrow-right" />}
        {item.reminder.Name}
      </span>
    );
  };

  render() {
    const {
      data,
      setReminderNodeExpanded,
      setReminderNodeCollapsed
    } = this.props;

    const items = Object.values(data);
    const roots = items.filter(treeItem => treeItem.level === 0);
    
    return (
      <div className={bem({ block })}>
        <TreeView 
          items={roots}
          itemsExpr="descendants"
          itemComponent={item => this.renderItem(item)}
          onItemClick={this.itemClick}
          onItemExpanded={item => setReminderNodeExpanded(item.itemData.reminder.Id)}
          onItemCollapsed={item => setReminderNodeCollapsed(item.itemData.reminder.Id)}
        />
      </div>
    );
  }
}
