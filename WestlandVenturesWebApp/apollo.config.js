module.exports = {
  client: {
    service: {
      name: 'wd-graph',
      url: 'http://localhost:4000/graphql',
      includes: ["./src/**/*.js"],
      excludes: ["**/__tests__/**"]
    }
  }
};