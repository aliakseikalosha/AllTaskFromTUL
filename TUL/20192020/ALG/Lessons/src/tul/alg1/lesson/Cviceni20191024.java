package tul.alg1.lesson;

import java.util.Scanner;

public class Cviceni20191024 {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        //CheckForDivision();
        //CanDrawTriangle();
        //DotQuadrant();
        //QuadraticEquation();
        SumAndAverege();
    }

    private static void SumAndAverege() {
        {
            System.out.print("Zadej pocet hodnot : ");
            int n = sc.nextInt();
            System.out.println("Zadej cisla.\n prikald : \"10 2 3\"");
            int x;
            int sum = 0;
            int count = 0;
            while (count < n) {
                x = sc.nextInt();
                sum += x;
                count++;
            }
            System.out.format("Soucet : %d \nPumer : %.3f", sum, ((float)sum) / count);
            System.out.println();
        }//varianta A
        {
            System.out.println("Zadej cisla a na konci dej nekladne cislo .\n prikald : \"10 2 3 -1\"");
            int x;
            int sum = 0;
            int count = 0;
            do {
                x = sc.nextInt();
                if (x > 0) {
                    sum += x;
                    count++;
                }
            } while (x > 0);
            System.out.format("Soucet : %d \nPumer : %.3f", sum, ((float)sum) / count);
            System.out.println();
        }//varianta B
    }

    private static void QuadraticEquation() {
        final float EPS = 1E-5F;
        System.out.println("Zadej koeficienty : \"a b c\"");
        float a = sc.nextFloat();
        float b = sc.nextFloat();
        float c = sc.nextFloat();


        if (Math.abs(a) < EPS) {
            if (Math.abs(b) < EPS) {
                System.out.println("To neni rovnice");
            } else {
                System.out.println("To neni kvadraticka rovnice x = " + (-c / b));
            }
        } else {
            float d = b * b - 4 * a * c;
            String x1, x2;
            if (d > 0) {
                x1 = Double.toString((-b + Math.sqrt(d)) / (2 * a));
                x2 = Double.toString((-b - Math.sqrt(d)) / (2 * a));
            } else {
                d = Math.abs(d);
                x1 = Double.toString((-b) / (2 * a)) + " + " + Double.toString((Math.sqrt(d)) / (2 * a)) + "i";
                x2 = Double.toString((-b) / (2 * a)) + " - " + Double.toString((Math.sqrt(d)) / (2 * a)) + "i";
            }
            System.out.format("x1 a x2 se rovna %s %s", x1, x2);
        }
    }

    private static void DotQuadrant() {
        System.out.println("Zadej souzadnice bodu : \"x y\"");
        float x = sc.nextFloat();
        float y = sc.nextFloat();
        int q = y >= 0 ? (x >= 0 ? 1 : 2) : (x > 0 ? 4 : 3);
        System.out.format("Bod se souřadnicy (%.3f,%.3f) leži v %d kvadrantu", x, y, q);
    }

    private static void CanDrawTriangle() {
        System.out.println("Zadej 3 strany : \"a b c\"");
        float a = sc.nextFloat();
        float b = sc.nextFloat();
        float c = sc.nextFloat();
        boolean check = (a + b) > c && (a + c) > b && (b + c) > a;//(a+b)>c && Math.abs(a-b)<c;
        System.out.println((check ? "Dá se " : "Nedá se ") + "sestrojit trojúhelnik.");
    }

    private static void CheckForDivision() {

        System.out.print("Zadej cislo : ");
        int n = sc.nextInt();
        System.out.print("Zadej delitel : ");
        int d = sc.nextInt();
        System.out.format("Cislo %d %s delitelne cislem %d", n, IsDivisor(n, d) ? "je" : "neni", d);
        System.out.println();
    }

    private static boolean IsDivisor(int a, int b) {
        return a % b == 0;
    }
}
