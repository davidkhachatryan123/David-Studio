export const environment = {
  production: true,
  config: {
    api: 'http://${hostname}/api',
    domain: '${hostname}'
  },
  sessionStorageConfig: {
    USER_KEY: 'auth-user',
    TOKEN_KEY: 'token'
  }
};
