package tul.alg1.lesson;

import java.util.Scanner;

public class ConvertTimeToMiliseconds {
    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        {
            System.out.print("Zadej počet hodin : ");
            int h = sc.nextInt();
            System.out.println();
            System.out.print("Zadej počet minut : ");
            int m = sc.nextInt();
            System.out.println();
            System.out.print("Zadej počet sekund : ");
            int s = sc.nextInt();
            System.out.println();
            System.out.print("Zadej počet milisekund : ");
            int ms = sc.nextInt();
            System.out.println();

            long msTotal = (((long) (h * 60 + m) * 60 + s) * 1000 + ms);
            System.out.println("Pocet milisekund : " + msTotal);
        }
        {
            System.out.print("Zadej počet milisisekund : ");
            long inputms = sc.nextLong();
            System.out.println();

            long h = inputms/(3600 * 1000);
            long m = inputms/(60 * 1000)%60;
            long s = inputms/(1000)%60;
            long ms = inputms%1000;

            System.out.format("%02d:%02:%02d.%03d", h,m,s,ms);
            System.out.println();
        }
    }
}
