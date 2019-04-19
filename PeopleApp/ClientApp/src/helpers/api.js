import { createChannel } from './channel';
import context from '../api/api';

export const createInsertEvents = () => {
  const channel = createChannel();

  context.card.arrayStore.on('inserted', (dataFromStore) => {
    channel.put(dataFromStore);
  });

  return channel;
};

export const createInsertAppealEvents = () => {
  const channel = createChannel();

  context.card.appealArrayStore.on('inserted', (dataFromStore) => {
    channel.put(dataFromStore);
  });

  return channel;
};

export const createLoadedEvents = () => {
  const channel = createChannel();

  context.card.arrayStore.on('loaded', (dataFromStore) => {
    channel.put(dataFromStore);
  });

  return channel;
};

export const createModifiedEvents = () => {
  const channel = createChannel();

  context.card.arrayStore.on('modified', (dataFromStore) => {
    channel.put(dataFromStore);
  });

  return channel;
};
