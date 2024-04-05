package com.example.gpstrackingdemo;

import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

public class ApiService {
    private String token;
    public String GetToken() {
        return token;
    }

    public Response Login(LoginData loginData){

        HttpURLConnection httpURLConnection = null;
        try {
            URL url = new URL("https://mybiking.azurewebsites.net/api/auth/login");
            httpURLConnection = (HttpURLConnection) url.openConnection();

            httpURLConnection.setRequestMethod("POST");
            httpURLConnection.setDoInput(true);
            httpURLConnection.setDoOutput(true);

            Gson gson = new Gson();
            String jsonInputString = gson.toJson(loginData);

            httpURLConnection.setRequestProperty("Content-Type", "application/json");

            try (OutputStream os = httpURLConnection.getOutputStream()) {
                // Replace with your JSON payload
                byte[] input = jsonInputString.getBytes("utf-8");
                os.write(input, 0, input.length);
            }

            int responseCode = httpURLConnection.getResponseCode();

            if(responseCode== HttpURLConnection.HTTP_OK){
                ResponseLoginApi responseLoginApi  = gson.fromJson(getHttpResponse(httpURLConnection),ResponseLoginApi.class);
                token = responseLoginApi.getMessage();
                return Result.Ok();
            }
            else{
                return Result.BadReuqest();
            }

        } catch (MalformedURLException e) {
            e.printStackTrace();
            return Result.BadReuqest();
        } catch (IOException e) {
            e.printStackTrace();
            return Result.BadReuqest();
        }finally {
            if (httpURLConnection != null) {
                httpURLConnection.disconnect();
            }
        }
    }
    private static String getHttpResponse(HttpURLConnection connection) throws IOException {
        StringBuilder response = new StringBuilder();
        BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));
        String inputLine;

        while ((inputLine = in.readLine()) != null) {
            response.append(inputLine);
        }

        in.close();

        return response.toString();
    }
    public Response AddRide(Ride ride){
        HttpURLConnection httpURLConnection = null;
        try {
            URL url = new URL("https://mybiking.azurewebsites.net/api/ride");
            httpURLConnection = (HttpURLConnection) url.openConnection();

            httpURLConnection.setRequestMethod("POST");
            httpURLConnection.setDoInput(true);
            httpURLConnection.setDoOutput(true);
            httpURLConnection.setRequestProperty("Authorization","Bearer "+token);
            Gson gson = new Gson();
            String jsonInputString = gson.toJson(ride);

            httpURLConnection.setRequestProperty("Content-Type", "application/json");

            try (OutputStream os = httpURLConnection.getOutputStream()) {
                // Replace with your JSON payload
                byte[] input = jsonInputString.getBytes("utf-8");
                os.write(input, 0, input.length);
            }

            int responseCode = httpURLConnection.getResponseCode();
            if(responseCode== HttpURLConnection.HTTP_OK){
                token  = getHttpResponse(httpURLConnection);
                return Result.Ok();
            }
            else{
                return Result.BadReuqest();
            }

        } catch (MalformedURLException e) {
            return Result.BadReuqest();
        } catch (IOException e) {
            return Result.BadReuqest();
        }finally {
            if (httpURLConnection != null) {
                httpURLConnection.disconnect();
            }
        }
    }
}
