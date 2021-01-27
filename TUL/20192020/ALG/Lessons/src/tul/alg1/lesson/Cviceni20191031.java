package tul.alg1.lesson;

import java.util.Scanner;

public class Cviceni20191031 {

    private static Scanner sc = new Scanner(System.in);

        public static void main(String[] args) {
        //SumAndAveragePositiveDouble();
        //SumAndAvergeCountDouble();
        //PrintFactorial();
        //CheckPrimeNumber();
        //DrawStarsLine();
        //DrawStarsSquare();
        DrawStarsTriangle();
    }

    private static void SumAndAveragePositiveDouble() {
        System.out.println("Soucet a prumer kladnych hodnot ");
        System.out.println("Druha varianta");
        System.out.println();

        double a;
        double sum;
        int count;

        sum = count = 0;

        do {
            System.out.print("Zadej cislo : ");
            a = sc.nextDouble();
            if (a > 0) {
                count++;
                sum += a;
            }
        } while (a > 0);

        System.out.println("Soucet : " + sum + "\n prumer : " + (sum / count));
    }

    private static void SumAndAvergeCountDouble() {
        System.out.println("Soucet a prumer  hodnot ");
        System.out.println("Druha varianta");
        System.out.println();

        double sum;
        System.out.print("Zadej pocet hodnot : ");
        int count = sc.nextInt();
        sum = 0;

        for (int i = 0; i < count; i++) {
            System.out.print("Zadej cislo " + (i + 1) + " : ");
            sum += sc.nextDouble();
        }

        System.out.println("Soucet : " + sum + "\nprumer : " + (sum / count));
    }

    private static void PrintFactorial() {
        System.out.print("Zadej čislo : ");

        int n = sc.nextInt();
        long f = 1;
        String message = "";
        for (int i = n; i > 1; i--) {
            f *= i;
            message += i + "*";
        }
        message += "1";
        System.out.println("Factorial " + n + "! se rovna : " + f + "\n" + n + "! = " + message + " = " + f);
    }

    private static void CheckPrimeNumber() {
        System.out.print("Zadej čislo : ");
        int n = sc.nextInt();
        boolean isPrime = true;
        for (int i = 2; i < n / 2; i++) {
            if (n % i == 0) {
                isPrime = false;
                break;
            }
        }
        System.out.println(n + " " + (isPrime ? "je" : "není") + " prvočislo");
    }

    private static void DrawStarsLine() {
        System.out.print("Zadej cislo : ");
        int n = sc.nextInt();
        for (int i = 0; i < n; i++) {
            System.out.print("* ");
        }
    }

    private static void DrawStarsSquare() {

        System.out.print("Zadej cislo : ");
        int n = sc.nextInt();

        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                System.out.print("* ");
            }
            System.out.println();
        }
    }

    private static void DrawStarsTriangle() {

        System.out.print("Zadej cislo : ");
        int n = sc.nextInt();

        for (int i = 0; i < n; i++) {
            for (int j = 0; j <= i; j++) {
                System.out.print("*");
            }
            System.out.println();
        }
        for (int i = 0; i < n; i++) {
            for (int k = 0; k < n - (i + 1); k++) {
                System.out.print(" ");
            }
            for (int j = 0; j <= i; j++) {
                System.out.print("*");
            }
            System.out.println();
        }
    }
}
