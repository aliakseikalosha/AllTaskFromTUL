package tul.alg1.lesson;

import java.util.Scanner;

public class Cviceni20191107 {
    private static Scanner sc = new Scanner(System.in);
    private final static double EPS = 1E-5;

    public static void main(String[] args) {
        //MinMaxPositiveNumbers();
        //MinMaxRealNumbers();
        //TestForDividableDecimal();
        //TestForAllDevidableDecimal();
        TestForGeometrySequence();
    }

    private static void MinMaxPositiveNumbers() {
        int min = Integer.MAX_VALUE;
        int max = Integer.MIN_VALUE;
        int a;
        System.out.print("Zadej cislo : ");
        while ((a = sc.nextInt()) > 0) {

            if (a > max) {
                max = a;
            }
            if (a < min) {
                min = a;
            }

            System.out.print("Zadej cislo : ");
        }
        System.out.println(String.format("min : %d a max %d", min, max));
    }

    private static void MinMaxRealNumbers() {
        double min = Double.POSITIVE_INFINITY;
        double max = Double.NEGATIVE_INFINITY;
        double a;

        System.out.print("Zadej počet čisel : ");
        int n = sc.nextInt();
        for (int i = 0; i < n; i++) {

            System.out.print(String.format("Zadej %d/%d čislo : ", i + 1, n));
            a = sc.nextDouble();
            if (a > max) {
                max = a;
            }
            if (a < min) {
                min = a;
            }
        }

        System.out.println(String.format("min : %f a max %f", min, max));
    }

    private static void TestForDividableDecimal() {
        boolean isDevidable = false;
        System.out.print("Zadej čislo : ");
        int c = sc.nextInt();
        System.out.print("Zadej počet hodnot : ");
        int n = sc.nextInt();
        int a;
        for (int i = 0; i < n; i++) {
            System.out.format("Zadej %d/%d cislo : ", i + 1, n);
            a = sc.nextInt();
            isDevidable |= a % c == 0;
        }
        System.out.println(isDevidable ? "Ano" : "Ne");
        System.out.println((isDevidable ? "Aspoń jedno čislo" : "Žadné čislo") + String.format(" je delitelné čislem %d", c));

    }

    private static void TestForAllDevidableDecimal() {
        boolean isDevidable = true;
        System.out.print("Zadej čislo : ");
        int c = sc.nextInt();
        System.out.print("Zadej počet hodnot : ");
        int n = sc.nextInt();
        int a;
        for (int i = 0; i < n; i++) {
            System.out.format("Zadej %d/%d cislo : ", i + 1, n);
            a = sc.nextInt();
            isDevidable &= a % c == 0;
        }
        System.out.println(isDevidable ? "Ano" : "Ne");
    }

    private static void TestForGeometrySequence() {
        boolean isSequence = true;
        System.out.print("Zadej pocet hodnot : ");
        int n = sc.nextInt();
        double b1, b2, q;

        b1 = q = 1;
        for (int i = 0; i < n; i++) {

            System.out.format("Zadej %d/%d  hodnotu : ", i + 1, n);
            if (i == 0) {
                b1 = sc.nextDouble();
            } else if (i == 1) {
                b2 = sc.nextDouble();
                q = b2 / b1;
                b1 = b2;
            } else {
                b2 = sc.nextDouble();
                isSequence &= isEqual(b2/b1, q);
                b1 = b2;
            }
        }

        System.out.println(isSequence ? "Ano" : "Ne");
    }

    private static boolean isEqual(double a, double b){
        return Math.abs(a-b) <= EPS;
    }
}
