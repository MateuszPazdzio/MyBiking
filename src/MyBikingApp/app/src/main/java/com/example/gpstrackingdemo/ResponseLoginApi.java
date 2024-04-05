package com.example.gpstrackingdemo;

public class ResponseLoginApi {
    private final String statuscode;
    private final String message;

    public ResponseLoginApi(String statuscode, String message) {
        this.statuscode = statuscode;
        this.message = message;
    }

    public String getMessage() {
        return message;
    }
}
