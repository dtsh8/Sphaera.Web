function isTruthy(value) {
  if (!value) return false;
  return ['1', 'true'].indexOf(value.toLowerCase()) >= 0;
}

// Warnings are errors in CI
const OFF = 'off';
const ERROR = 'error';
const WARNING = isTruthy(process.env.CI) ? ERROR : 'warn';

module.exports = {
  "extends": "airbnb",
  "parser": "babel-eslint",
  "env": {
    "browser": true,
    "node": true,
    "es6": true,
    "jest": true
  },
  "rules": {
    "import/no-unresolved": OFF,
    "import/extensions": OFF,
    "no-trailing-spaces": OFF,
    "no-return-assign": OFF,
    "jsx-a11y/anchor-is-valid": OFF,
    "eol-last": OFF,
    "no-param-reassign": OFF,
    "import/prefer-default-export": OFF,
    "arrow-body-style": OFF,
    "function-paren-newline": OFF,
    "react/prefer-stateless-function": OFF,
    "react/jsx-filename-extension": OFF,
    "comma-dangle": OFF,
    "eqeqeq": [WARNING, "allow-null"],
    "jsx-a11y/no-static-element-interactions": OFF,
    "jsx-a11y/click-events-have-key-events": OFF,
    // "import/imports-first": OFF,
    "indent": [WARNING, 2, {"SwitchCase": 1}],
    "max-len": [ERROR, {
      "ignorePattern": "^import|^export",
      "code": 140,
      "tabWidth": 2
    }],
    // "no-console": [WARNING, {"allow": ["warn", "error"]}],
    // "no-debugger": WARNING,
    // "no-fallthrough": WARNING,
    // "no-unreachable": WARNING,
    // "no-unused-vars": [WARNING, {"vars": "all", "args": "none"}],
    // "no-var": ERROR,
    "prefer-const": WARNING,
    // "react/prop-types": [WARNING, {"ignore": ["className"]}],
    "react/prop-types": OFF,
    "semi": [ERROR, "always"],
  },
};