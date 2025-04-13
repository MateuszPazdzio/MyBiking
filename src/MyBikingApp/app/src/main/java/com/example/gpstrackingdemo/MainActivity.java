package com.example.gpstrackingdemo;

import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.hardware.*;
import android.location.Address;
import android.location.Geocoder;
import android.location.Location;
import android.media.MediaPlayer;
import android.os.Build;
import android.view.View;
import android.widget.Button;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;
import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import android.os.Bundle;
import androidx.core.app.ActivityCompat;
import com.google.android.gms.location.*;
import com.google.android.gms.tasks.OnSuccessListener;

import java.text.DecimalFormat;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

//import io.appium.java_client.AppiumDriver;
//import io.appium.java_client.MobileElement;
//import io.appium.java_client.android.AndroidDriver;
//import io.appium.java_client.remote.MobileCapabilityType;
//import org.openqa.selenium.remote.DesiredCapabilities;


public class MainActivity extends AppCompatActivity implements SensorEventListener {

    public static final long INTERVAL_TIME = 4;
    public static final int PERMISSIONS_FINE_LOCATION = 99;
    private double lastLongitude=0.00, lastLatitude=0.00, startTime =0.00, distaceTraveledBetwee2Locations, RideDistance;
//    private TextView rotateZ;
    private SensorManager sensorManager;
    private List<Sensor> deviceSensors;
    private Button btnStart;
    private MediaPlayer mediaPlayer;
    //for calculating max x rotation, while doing wheelie
    private List<Float> rotaionXList = new ArrayList<>();
    TextView tv_speed, tv_distance, tv_time,tv_totalWheelieDistance, tv_lastWheeleDistance,tv_lastWheeleTime, rotateY;
    boolean isWheeleMode = false, isRideModeOn = false;
    LocationRequest locationRequest;
    FusedLocationProviderClient fusedLocationProviderClient;
    LocationCallback locationCallBack;
    private  Ride ride;
    private WheeleRide wheeleRide;
    private ArrayList<Double> rotationsValues = new ArrayList<Double>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        mediaPlayer = MediaPlayer.create(this, R.raw.load_gun);
        rotateY = findViewById(R.id.rotateYVal);

        sensorManager=(SensorManager)getSystemService(Context.SENSOR_SERVICE);
        deviceSensors = sensorManager.getSensorList(Sensor.TYPE_ALL);

        tv_distance = findViewById(R.id.tv_distance);
        tv_speed = findViewById(R.id.tv_speed);
        btnStart = findViewById(R.id.btnStart);
        tv_time=findViewById(R.id.tv_time);
        tv_totalWheelieDistance=findViewById(R.id.tv_totalWheelieDistance);
        tv_lastWheeleDistance=findViewById(R.id.tv_lastWheelieDistance);
        tv_lastWheeleTime=findViewById(R.id.tv_lastWheeleTime);
        locationRequest = new LocationRequest();

        locationRequest.setInterval(1000*INTERVAL_TIME);
        locationRequest.setFastestInterval(5000);
        locationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
        //event is triggered whenever the update interval is met
        locationCallBack = new LocationCallback() {

            @Override
            public void onLocationResult(@NonNull LocationResult locationResult) {
                super.onLocationResult(locationResult);
                //save the location
                Location location = locationResult.getLastLocation();
                updateUIValues(location);
            }
        };


        btnStart.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String x =btnStart.getText().toString().trim();
                if(btnStart.getText().toString().trim().toLowerCase().equals("start")){
                    isRideModeOn = true;
                    RideDistance = 0.00;
                    startLocationUpdates();
                    btnStart.setText("STOP");
                    btnStart.setBackgroundColor(getResources().getColor(R.color.red));
                //Case its Stop
                }else{
                    isRideModeOn = false;
                    tv_lastWheeleDistance.setText(tv_totalWheelieDistance.getText().toString());
                    btnStart.setText("START");
                    btnStart.setBackgroundColor(getResources().getColor(R.color.green));
                    tv_lastWheeleTime.setText(tv_time.getText().toString());
                    stopLocationUpdates();
                    RideDistance = 0.00;
                }
            }
        });

        updateGPS();
    }

    private void stopLocationUpdates() {

        tv_speed.setText("0.00");
        tv_distance.setText("0.00");
        ride.setEndingDateTime(LocalDateTime.now().toString());
        ride.setDistance(RideDistance);
        new FetchData(ride).execute();

        fusedLocationProviderClient.removeLocationUpdates(locationCallBack);
    }

    private void startLocationUpdates() {

//        tv_updates.setText("Location is being tracked");
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }
        fusedLocationProviderClient.requestLocationUpdates(locationRequest, locationCallBack, null);

        ride = new Ride();
//        ride.setBikeId("1");
        ride.setStartingDateTime(LocalDateTime.now().toString());
        updateGPS();
    }


    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);

        switch(requestCode){
            case PERMISSIONS_FINE_LOCATION:
                if(grantResults[0]==PackageManager.PERMISSION_GRANTED){
                    updateGPS();
                }else{
                    Toast.makeText(this,"This app requires permission to be granted in order to work properlly",
                            Toast.LENGTH_SHORT).show();
                    finish();
                }
                break;
        }
    }

    private void updateGPS(){
        fusedLocationProviderClient = LocationServices.getFusedLocationProviderClient(MainActivity.this);
        if(ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION)== PackageManager.PERMISSION_GRANTED){
            //user provided the permission
            fusedLocationProviderClient.getLastLocation().addOnSuccessListener(this, new OnSuccessListener<Location>() {
                @Override
                public void onSuccess(Location location) {
                    //put the values of location. Put data into UI components
                    updateUIValues(location);
                }
            });
        }else{
            //permissions not granted
           if(Build.VERSION.SDK_INT>=Build.VERSION_CODES.M){
               requestPermissions(new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, PERMISSIONS_FINE_LOCATION);
           }
        }
    }

    private void updateRideDetails(Location location) {
        Geocoder geocoder = new Geocoder(MainActivity.this);
        String address;
        if(ride!=null){
            try{
                List<Address> addresses = geocoder.getFromLocation(location.getLatitude(),location.getLongitude(),1);
                address=addresses.get(0).getAddressLine(0);
            }
            catch (Exception e){
                address="Error";
            }
            Point point = new Point(location.getLatitude(),location.getLongitude(),address);
            ride.AddPoint(point);
//
            distaceTraveledBetwee2Locations = calcDistance(point.getLatitude(),point.getLongitude());
            if(distaceTraveledBetwee2Locations<2.0){
                distaceTraveledBetwee2Locations = 0.00d;
            }
            DecimalFormat df = new DecimalFormat("#.###");
//            String val = df.format((distaceTraveledBetwee2Locations/1000));
            String distanceFormatted =String.format("%.3f",distaceTraveledBetwee2Locations/1000);
            distanceFormatted=distanceFormatted.replace(",",".");

            double roundedNumberDouble = 0.0;
            roundedNumberDouble = Double.parseDouble(distanceFormatted);

            RideDistance += roundedNumberDouble;

            if(wheeleRide!=null){
                DecimalFormat dfSpeed = new DecimalFormat("#.##");
                float speed = Math.round(location.getSpeed());
//                float speed = Float.parseFloat(dfSpeed.format(location.getSpeed()));
//                float altitude = Float.parseFloat(df.format(location.getAltitude()));
                float altitude = Math.round(location.getAltitude());

                wheeleRide.AddIWheeleItem(new WheeleItem(point,speed,altitude,address,distaceTraveledBetwee2Locations, Collections.max(rotaionXList)));
            }
        }

    }

    private void updateUIValues(Location location) {

        if (location == null) {
            // Optionally log or show something to user
            return;
        }

        updateRideDetails(location);
        rotaionXList.clear();

        if(isWheeleMode){
            tv_totalWheelieDistance.setText(String.valueOf(Double.parseDouble(tv_totalWheelieDistance.getText().toString())+distaceTraveledBetwee2Locations));
        }
        tv_distance.setText(String.valueOf(RideDistance));


        if(location.hasSpeed() && isRideModeOn){
            tv_speed.setText(String.valueOf(location.getSpeed()*3.6f));
        }
        else{
            tv_speed.setText(String.valueOf("0.00"));
        }

        Geocoder geocoder = new Geocoder(MainActivity.this);
        try{
            List<Address> addresses = geocoder.getFromLocation(location.getLatitude(),location.getLongitude(),1);
        }
        catch (Exception e){
//            tv_address2.setText("Unable to get street address");
        }

        MyApplication myApplication = (MyApplication) getApplicationContext();
        //show the number of waypoints saved
    }

    private double calcDistance(double latitude, double longitude) {
        if(lastLatitude!=0.00 && lastLongitude!=0.00){

            double distance = Math.sqrt(Math.pow(Math.abs(lastLatitude - latitude), 2) + Math.pow(Math.abs(lastLongitude - longitude), 2))*73*1000;
            lastLongitude=longitude;
            lastLatitude=latitude;
            return distance;
        }
        lastLongitude=longitude;
        lastLatitude=latitude;
        return 0.0;
    }

    @Override
    public void onSensorChanged(SensorEvent sensorEvent) {

        if(sensorEvent.sensor.getType()==Sensor.TYPE_GAME_ROTATION_VECTOR && isRideModeOn){

            String roundedNumberStr = String.format("%.3f", sensorEvent.values[0]);
            roundedNumberStr = roundedNumberStr.replace(",", ".");
            float x = 0f;
            x = Float.parseFloat(roundedNumberStr);//Rotation around x-axis (roll)

            if(x>0.25f && startTime==0.00){
                rotaionXList.add(x);
                wheeleRide = new WheeleRide(LocalDateTime.now().toString());
                if (mediaPlayer != null && !mediaPlayer.isPlaying()) {
                    mediaPlayer.start(); // Start playing the sound
                }

                isWheeleMode=true;
                startTime = System.currentTimeMillis();
                tv_time.setText(String.valueOf(startTime));
                rotateY.setText("Wheelie On");
                tv_totalWheelieDistance.setText((tv_totalWheelieDistance.getText().toString()));
            }
            else if(x>0.25f){
                rotaionXList.add(x);
                double timeDiff = (System.currentTimeMillis()-startTime)/1000;
                tv_time.setText(String.valueOf(timeDiff));
            }
            else{
                if(isWheeleMode){
                    wheeleRide.setDurationTime(tv_time.getText().toString());
                    wheeleRide.setEndingDateTime(LocalDateTime.now().toString());

                    String totalDistance = tv_totalWheelieDistance.getText().toString();
                    totalDistance=totalDistance.substring(0,totalDistance.indexOf(".")+getCountOfDigitsAfterComma(totalDistance));
                    double totalWheelieDistance = Double.parseDouble(totalDistance);

                    if(Double.parseDouble(tv_time.getText().toString())>5.0 && totalWheelieDistance>=0.0){
                        wheeleRide.setDistance(totalWheelieDistance);
                        ride.AddWheeleRide(wheeleRide);
                    }
                    wheeleRide = null;
                    tv_lastWheeleDistance.setText(tv_totalWheelieDistance.getText().toString());
                    tv_lastWheeleTime.setText(tv_time.getText().toString());
                }
                isWheeleMode=false;

                startTime=0.00;
                tv_time.setText(String.valueOf(0.00));
                rotateY.setText("Wheelie Off");
                tv_totalWheelieDistance.setText(String.valueOf(0.00));
            }

        }
    }

    private int getCountOfDigitsAfterComma(String totalDistance) {
        if(totalDistance.contains(".")){
            int charCount = totalDistance.substring(totalDistance.indexOf(".")+1).toCharArray().length;
            return charCount>3?4:charCount+1;
        }
        return 0;
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int i) {

    }

    @Override
    protected void onResume() {
        super.onResume();
        sensorManager.registerListener((SensorEventListener) this,sensorManager.getDefaultSensor(Sensor.TYPE_GAME_ROTATION_VECTOR),SensorManager.SENSOR_DELAY_NORMAL);
    }

    @Override
    protected void onPause() {
        super.onPause();
        sensorManager.unregisterListener((SensorListener) this);
    }
}



