package tul.alg1.lesson;

import java.io.IOException;
import java.util.Scanner;


public class Cviceni20191219 {
    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) throws IOException {
        //dotExsampleSeparated();
        //dotExsampleOneArray();
        new Cviceni20191219().dotExampleClass();
    }

    private static void dotExsampleSeparated() {
        double[] x;
        double[] y;
        System.out.print("Zadej pocet bodu : ");
        int n = sc.nextInt();
        x = new double[n];
        y = new double[n];
        for (int i = 0; i < n; i++) {
            System.out.print("Zadej X a Y : ");
            x[i] = sc.nextDouble();
            y[i] = sc.nextDouble();
        }
        int perimeter = 0;
        for (int i = 1; i < n; i++) {
            perimeter += Distance(x[i - 1], y[i - 1], x[i], y[i]);
        }
        perimeter += Distance(x[0], y[0], x[n - 1], y[n - 1]);
        System.out.println("Obvod je " + perimeter);
    }

    private static void dotExsampleOneArray() {
        double[][] dots;
        System.out.print("Zadej pocet bodu : ");
        int n = sc.nextInt();
        dots = new double[n][2];
        for (int i = 0; i < n; i++) {
            System.out.print("Zadej X a Y : ");
            dots[i][0] = sc.nextDouble();
            dots[i][1] = sc.nextDouble();
        }
        int perimeter = 0;
        for (int i = 1; i < n; i++) {
            perimeter += Distance(dots[i - 1][0], dots[i - 1][1], dots[i][0], dots[i][1]);
        }
        perimeter += Distance(dots[0][0], dots[0][1], dots[n - 1][0], dots[n - 1][0]);
        System.out.println("Obvod je " + perimeter);
    }

    private void dotExampleClass() {
        Dot[] dots;
        System.out.print("Zadej pocet bodu : ");
        int n = sc.nextInt();
        dots = new Dot[n];
        for (int i = 0; i < n; i++) {
            System.out.print("Zadej X a Y : ");
            dots[i] = new Dot(sc.nextDouble(), sc.nextDouble());
        }
        int perimeter = 0;
        for (int i = 1; i < n; i++) {
            perimeter+=dots[i-1].Distance(dots[i]);
        }
        perimeter+=dots[n-1].Distance(dots[0]);
        System.out.println("Obvod je " + perimeter);
    }

    private static double Distance(double x1, double y1, double x2, double y2) {
        double x = x1 - x2;
        double y = y1 - y2;
        return Math.sqrt(x * x + y * y);
    }

    private class Dot {
        public double x;
        public double y;

        public Dot(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public double Distance(Dot b) {
            return StrictMath.hypot(x - b.x, y - b.y);
        }
    }

}
