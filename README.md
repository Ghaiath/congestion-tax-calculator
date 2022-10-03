# Congestion Tax Calculator

Run `docker-compose up` up to start the services

This will seed the MongoDB with the initial data.

Not all endpoints implemented. Only get of tax rules and get congestion tax.

Once you run `docker-compose up`, surf to http://localhost:8000/swagger/index.html


Input:

```
{
  "vehicleType": "CAR",
  "dates": [
    "2013-01-14 21:00:00",
    "2013-01-15 21:00:00",
    "2013-02-07 06:23:27",
    "2013-02-07 15:27:00",
    "2013-02-08 06:27:00",
    "2013-02-08 06:20:27",
    "2013-02-08 14:35:00",
    "2013-02-08 15:29:00",
    "2013-02-08 15:47:00",
    "2013-02-08 16:01:00",
    "2013-02-08 16:48:00",
    "2013-02-08 17:49:00",
    "2013-02-08 18:29:00",
    "2013-02-08 18:35:00",
    "2013-03-26 14:25:00",
    "2013-03-28 14:07:27"
  ]
}
```
And got the follwing response: 
```
{
  "vehicleType": "CAR",
  "taxes": {
    "2013-01-14T00:00:00": 0,
    "2013-01-15T00:00:00": 0,
    "2013-02-07T00:00:00": 21,
    "2013-02-08T00:00:00": 60,
    "2013-03-26T00:00:00": 8,
    "2013-03-28T00:00:00": 0
  },
  "total": 89
}
```