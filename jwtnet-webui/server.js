const express = require('express');
const cors = require('cors');
const app = express();

app.use(cors());

app.use('/login', (req, res) => {
    res.send({
        token: 'test123'
    });
});

app.listen(7088, () => console.log('API is running on https://localhost:7088/api/user/login'));