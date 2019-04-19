export const getUserInfo = () => {
  return {
    fio: 'Иванов Иван Иванович',
    login: 'ivanov',
    phone: '+7 926 123-45-67',
    email: {
      address: 'ivanov@example.ru',
      activate: false
    },
    location: {
      coords: [47.2, 39.75],
      zoom: 13
    }
  };
};
