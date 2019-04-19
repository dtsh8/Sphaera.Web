import { LOAD_STATISTIC, LOADED_STATISTIC } from './constants';

export const getStatistic = () => {
  return {
    type: LOAD_STATISTIC
  };
};

export const statisticLoaded = (statistic) => {
  return {
    type: LOADED_STATISTIC,
    payload: {
      statistic
    }
  };
};