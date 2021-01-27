package tul.alg1.lesson;

import java.util.Scanner;

public class CubeVolume {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        System.out.println("Zadej starany kvadry");
        float a = sc.nextFloat(), b = sc.nextFloat(), c = sc.nextFloat();

        float volumeP = a * b * c;
        System.out.println("");

        double lenghtACube = Math.pow(volumeP, 1.0 / 3.0);

        double r = Math.pow(volumeP * 3 / 4 / Math.PI, 1.0 / 3.0);


    }
}
