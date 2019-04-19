export const handleAxiosError = (promise) => {
  return promise
    .then(response => response.data)
    .catch(error => error.response);
};