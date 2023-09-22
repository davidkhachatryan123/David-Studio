export const environment = {
  production: false,
  identity: {
    authority: 'https://localhost:5011',
    scopes: [
      'openid', 'profile', 'offline_access', 'email',
      'users', 'portfolio', 'pricing', 'storage', 'messenger'
    ]
  },
  api: 'http://localhost:8082/api/v1'
};
