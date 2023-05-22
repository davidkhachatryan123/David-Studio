export const environment = {
  production: true,
  config: {
    api: 'http://${hostname}/api',
  },
  sessionStorageConfig: {
    USER_KEY: 'auth-user',
    TOKEN_KEY: 'token'
  }
};
