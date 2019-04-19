import { Iterable } from 'immutable';

export const isImmutable = (data) => {
  return Iterable.isIterable(data);
};

export const convertFromImmutableToJS = (elt) => {
  if (isImmutable(elt)) {
    return elt.toJS();
  }

  return elt;
};


// Get filter params like a string for the filter panel
export const getFilterParams = (filterValue, columns) => {
  const getFilterParamsForOneColumn = (columnFilterValue) => {
    if (Array.isArray(columnFilterValue[2])) {
      const values = columnFilterValue[2].reduce((result, item) => {
        return `«${result}», «${item}»`;
      });

      return `${values}`;
    }
    return `«${columnFilterValue[2]}»`;
  };

  if (filterValue[1] === 'and') {
    let result = '';
    filterValue.forEach((item, index) => {
      if (index !== 0 && index % 2 === 0) {
        result = `${result} / ${columns.find(column => column.dataField === item[0]).caption}: ${getFilterParamsForOneColumn(item)}`;
      }
      if (index === 0) {
        result = `${columns.find(column => column.dataField === item[0]).caption}: ${getFilterParamsForOneColumn(item)}`;
      }
    });
    return result;
  }
  return `${columns.find(column => column.dataField === filterValue[0]).caption}: ${getFilterParamsForOneColumn(filterValue)}`;
};

export const sagasDelay = ms => new Promise(res => setTimeout(res, ms));

export const base64ToArrayBuffer = (base64) => {
  const binaryString = window.atob(base64);
  const bytes = new Uint8Array(binaryString.length);

  return bytes.map((b, i) => {
    return binaryString.charCodeAt(i);
  });
};