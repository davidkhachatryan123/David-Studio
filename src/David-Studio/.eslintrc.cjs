/* eslint-env node */
module.exports = {
  parser: '@typescript-eslint/parser',
  root: true,
  parserOptions: {
    createDefaultProgram: true,
  },
  overrides: [
    {
      files: ['*.ts', '*.js'],
      parserOptions: {
        project: require.resolve('./tsconfig.json'),
        createDefaultProgram: true,
      },
      extends: ['plugin:@angular-eslint/recommended', 'plugin:@angular-eslint/template/process-inline-templates'],
    },
    {
      files: ['*.html'],
      extends: ['plugin:@angular-eslint/template/recommended'],
    },
    {
      files: ['*.md'],
      extends: ['plugin:markdown/recommended'],
    },
  ],
};