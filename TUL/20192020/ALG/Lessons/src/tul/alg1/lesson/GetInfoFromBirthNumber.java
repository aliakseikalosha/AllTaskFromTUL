package tul.alg1.lesson;

import java.util.Scanner;

public class GetInfoFromBirthNumber {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        int a = 4;
        System.out.println((a++)%5);
        a = 4;
        System.out.println((++a)%5);

        System.out.print("Zadej rodné čislo : ");
        long c = sc.nextLong();//70 60 20 7777

        long y = c / 1_00_00_0000;
        long m = c / 1_00_0000 % 100;
        long d = c / 1_0000 % 100;

        String sex = "Je to " + (m > 50 ? "zena" : "muz") + ".";
        String date = String.format("Datum narozeni %d.%d.%d.", d, m % 50, y < 19 ? y + 2000 : y + 1900);
        System.out.println(date);
        System.out.println(sex);
        System.out.println("Cislo je " + (c % 11 == 0 ? "platne" : "neplatne") + " rodne cislo");
    }

}
