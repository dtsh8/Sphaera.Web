import { cloneDeep, filter } from 'lodash';

const assignDefaultVal = (val, results) => {
  if (Object.keys(val).length) {
    return [val.name, ...results.map(({ name }) => name)];
  }
  return results.map(({ name }) => name);
};

export const getDictionaryList = (obj, defaultVal = {}) => {
  if (!obj) {
    return [];
  }

  const { results, loading } = obj;

  if (loading || !results) {
    return [];
  }

  return assignDefaultVal(defaultVal, results);
};

export const initResultsWithDictionaries = ({ statuses, types, dataMap }) => {
  return (statuses && types && dataMap && dataMap.data)
    ? {
      ...dataMap,
      data: dataMap.data.length
        ? dataMap.data.map(r => ({
          ...r,
          status: statuses.results.find(s => (s.id === r.status) || (s.id === r.stateId)) ||
            {
              code: '',
              description: '',
              id: 1,
              name: r.status
            },
          type: types.results.find(type => type.code === r.cardIndexCode) || {
            caseTypeId: 1,
            code: '',
            description: '',
            indexName: '',
            name: '',
            orderNo: 1,
            parentCode: null,
          },
        }))
        : []
    }
    : {
      data: [],
      count: 0,
      loading: false
    };
};

export const initIncidentTypes = (types) => {
  types = cloneDeep(types);
  const topLevel = filter(types, { parentCode: null });

  return topLevel.map((topLevelItem) => {
    const secondLevel = filter(types, { parentCode: topLevelItem.code });

    topLevelItem.children = secondLevel.map((secondLevelitem) => {
      const thirdLevel = filter(types, { parentCode: secondLevelitem.code });

      secondLevelitem.children = thirdLevel.map(thirdLevelItem => thirdLevelItem);

      return secondLevelitem;
    });

    return topLevelItem;
  });
};
