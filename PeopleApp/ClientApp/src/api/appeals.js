import context from './api';

export const connectAppealsJournal = () => {
  const filter = {
    incidentId: '', // Идентификатор происшествия (для поиска связанных карточек)
    dispatchServiceIds: [], // Идентификаторы ЕДДС (если не указано - все)
    serviceTypeIds: [], // Идентификаторы типов служб (если не указано - все)
    exceptServiceTypeIds: false, // Признак инверсии (для исключения) типов служб указанных в параметре ServiceTypeIds
    stateIds: [], // Идентификаторы статусов происшествий
    exceptStateIds: false, // Признак инверсии (для исключения) статусов происшествий указанных в параметре StateIds
    cardIndexCode: '', // Код типа (индекса) происшествия
    createdFrom: null, // Начало периода выборки карточек реагирования
    createdTill: null, // Окончание периода выборки карточек реагирования
    isDanger: false, // Признак чрезвычайной ситуации
    latitude: 0.0, // Широта точки привязки (для поиска по удалению от точки)
    longitude: 0.0, // Долгота точки привязки (для поиска по удалению от точки)
    distance: 0.0, // Дистанция поиска (для поиска по удалению от точки)
    mapExtent: '', // Экстент карты
    isOverdue: false, // Признак отклонения от сроков реагирования
    organizationCode: '', // Код организации (для внешних служб реагирования)
    userLogin: '' // Логин пользователя (для выборки его карточек)
  };

  context.card.getAppealCards(filter);
};

export const sendAppeal = (data) => {
  const {
    notes,
    typeCode,
    typeName,
    address
  } = data;

  // Запрос на реагирование
  const appealData = {
    claim: {
      message: notes // Текст заявки
    },
    address,
    cards: [{
      cardIndexCode: typeCode, // id типа происшествия
      cardIndexName: typeName // Название типа происшествия
    }] // Карточки
  };

  return context.incident.set(appealData);
};
