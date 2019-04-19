export const TREE_ITEM_TYPE_FOLDER = 1;
export const TREE_ITEM_TYPE_DOCUMENT = 2;

export const getRemindersTree = (reminderList) => {
  if (!reminderList) {
    return reminderList;
  }
  const tree = {};
  reminderList.forEach((item) => {
    const treeItem = {
      reminder: item,
      descendants: [],
      level: undefined
    };
    tree[item.Id] = treeItem;

    return treeItem;
  });

  Object.values(tree)
    .filter(treeItem => treeItem.reminder.ParentId && tree[treeItem.reminder.ParentId])
    .forEach(treeItem => tree[treeItem.reminder.ParentId].descendants.push(treeItem));
  const setLevel = (treeItem, level) => {
    treeItem.level = level;
    treeItem.descendants.forEach(descendant => setLevel(descendant, level + 1));
  };

  Object.values(tree).filter(treeItem => treeItem.reminder.ParentId === null)
    .forEach(rootItem => setLevel(rootItem, 0));
  return tree;
};
