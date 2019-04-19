export const getStatuses = () => {
  return {
    count: 3,
    results: [
      {
        id: 0,
        title: 'Новое',
        value: 'new'
      },
      {
        id: 1,
        title: 'В работе',
        value: 'work'
      },
      {
        id: 2,
        title: 'Реагирование завершено',
        value: 'done'
      }
    ]
  };
};

export const getTypes = () => {
  return {
    count: 2,
    results: [
      {
        id: 0,
        title: 'Аварийные ветки, деревья'
      },
      {
        id: 1,
        title: 'Хищение чужого имущества'
      }
    ]
  };
};

export const getPeriods = () => {
  return {
    count: 5,
    results: [
      {
        id: 0,
        name: 'За сегодня',
        value: 0
      },
      {
        id: 1,
        name: 'За неделю',
        value: 7
      },
      {
        id: 2,
        name: 'За месяц',
        value: 30
      },
      {
        id: 3,
        name: 'За квартал',
        value: 90
      },
      {
        id: 4,
        name: 'За год',
        value: 365
      }
    ]
  };
};
