import express from "express";
import axios from 'axios';
const express = require("express");
const app = express();
const port = process.env.PORT || 3000;

const API_KEY = 'rnd_lP2JybF3lm5YcatvP48DD7Iz6zbP';
let result;

axios.get("https://api.render.com/v1/services", {
  headers: {
    'Authorization': `Bearer ${API_KEY}`
  }
})
  .then(response => {
    result = response.data;
  })
  .catch(error => {
    console.error('Error:', error);
  });

app.get("/", (req, res) => res.type('JSON').send(result));

app.listen(port, () => console.log(`app listening on port ${port}!`));