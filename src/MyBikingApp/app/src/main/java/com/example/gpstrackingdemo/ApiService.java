package com.example.gpstrackingdemo;

import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import javax.net.ssl.*;
import java.security.cert.X509Certificate;

public class ApiService {
    private String token;
    public String GetToken() {
        return token;
    }

    public Response Login(LoginData loginData){

        HttpURLConnection httpURLConnection = null;
        try {
            trustAllHosts();
            URL url = new URL("https://x50gtstang.gotdns.ch:8080/api/auth/login");
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
    // Add this before making the HTTPS request
    private void trustAllHosts() {
        try {
            TrustManager[] trustAllCerts = new TrustManager[] {
                    new X509TrustManager() {
                        public java.security.cert.X509Certificate[] getAcceptedIssuers() {
                            return new X509Certificate[]{};
                        }
                        public void checkClientTrusted(X509Certificate[] chain, String authType) {}
                        public void checkServerTrusted(X509Certificate[] chain, String authType) {}
                    }
            };

            SSLContext sc = SSLContext.getInstance("TLS");
            sc.init(null, trustAllCerts, new java.security.SecureRandom());
            HttpsURLConnection.setDefaultSSLSocketFactory(sc.getSocketFactory());

            HttpsURLConnection.setDefaultHostnameVerifier(new HostnameVerifier() {
                public boolean verify(String hostname, SSLSession session) {
                    return true; // accept all hostnames
                }
            });
        } catch (Exception e) {
            e.printStackTrace();
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

            trustAllHosts();
            URL url = new URL("https://x50gtstang.gotdns.ch:8080/api/ride");
            httpURLConnection = (HttpURLConnection) url.openConnection();
            httpURLConnection.setRequestMethod("POST");
            httpURLConnection.setDoInput(true);
            httpURLConnection.setDoOutput(true);
            httpURLConnection.setRequestProperty("Authorization","Bearer "+token);
            httpURLConnection.setRequestProperty("Content-Type", "application/json");

            Gson gson = new Gson();
            String jsonInputString = gson.toJson(ride);

            try (OutputStream os = httpURLConnection.getOutputStream()) {
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
