#!/bin/bash
nswag swagger2csclient /namespace:GeneratedServerAPI /input:'http://localhost:8080/api/openapi.json' /output:GeneratedServerApi.cs
