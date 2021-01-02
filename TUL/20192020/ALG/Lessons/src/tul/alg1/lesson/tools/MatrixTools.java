package tul.alg1.lesson.tools;

import java.util.Scanner;

public class MatrixTools {
    /**
     * Soucet 2 matice
     *
     * @param a prvni matice
     * @param b druha matice
     * @return matice c = a + b
     */
    public static double[][] add(double[][] a, double[][] b) {
        if (a.length != b.length || a[0].length != b[0].length) {
            return null;
        }
        if (a.length == 0 || a[0].length == 0) {
            return null;
        }
        double[][] res = new double[a.length][a[0].length];
        for (int i = 0; i < a.length; i++) {
            for (int j = 0; j < a[i].length; j++) {
                res[i][j] = a[i][j] + b[i][j];
            }
        }
        return res;
    }

    /**
     * Soucin 2 matic
     *
     * @param a prvni matice
     * @param b druha matice
     * @return matice c = a * b
     */
    public static double[][] mult(double[][] a, double[][] b) {
        if (a[0].length != b.length) {
            return null;
        }
        if (a[0].length == 0) {
            return null;
        }
        if (b.length == 0) {
            return null;
        }
        double[][] res = new double[a[0].length][b.length];
        for (int i = 0; i < res.length; i++) {
            for (int j = 0; j < res[i].length; j++) {
                for (int k = 0; k < res.length; k++) {
                    res[i][j] += a[k][j] * b[j][k];
                }
            }
        }
        return res;
    }

    public static double[][] random(int x, int y) {
        double[][] res = new double[x][y];
        for (int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                res[i][j] = Math.random();
            }
        }
        return res;
    }

    public static double[][] mult(double a, double[][] b) {
        double[][] res = new double[b.length][b[0].length];
        for (int i = 0; i < b.length; i++) {
            for (int j = 0; j < b[i].length; j++) {
                res[i][j] = b[i][j] * a;
            }
        }
        return res;
    }

    public static String print(double[][] a) {
        String s = "";
        if (a == null) {
            s = "null";
        }
        for (double[] y : a) {
            for (double x : y) {
                s += String.format("%.3f ", x);
            }
            s += "\n";
        }
        return s;
    }

    public static String print(int[][] a) {
        String s = "";
        if (a == null) {
            s = "null";
        }
        for (int[] y : a) {
            for (int x : y) {
                s += String.format("%d ", x);
            }
            s += "\n";
        }
        return s;
    }

    public static double[][] readMatrix(Scanner sc) {
        System.out.print("Zadej pocet sloupcu : ");
        int x = sc.nextInt();
        System.out.print("Zadej pocet radku : ");
        int y = sc.nextInt();
        double[][] a = new double[x][y];
        for (int i = 0; i < a.length; i++) {
            for (int j = 0; j < a[i].length; j++) {
                System.out.print("Zadej hodnotu  prvku [" + i + "," + j + "]");
                a[i][j] = sc.nextDouble();
            }
        }
        return a;
    }
    public static int[][] readIntMatrix(Scanner sc) {
        System.out.print("Zadej pocet sloupcu : ");
        int x = sc.nextInt();
        System.out.print("Zadej pocet radku : ");
        int y = sc.nextInt();
        int[][] a = new int[x][y];
        for (int i = 0; i < a.length; i++) {
            for (int j = 0; j < a[i].length; j++) {
                System.out.print("Zadej hodnotu  prvku [" + i + "," + j + "]");
                a[i][j] = sc.nextInt();
            }
        }
        return a;
    }

    public static double absMax(double[][] m){
        double max = -Double.MAX_VALUE;
        for (int i = 0; i < m.length; i++) {
            for (int j = 0; j < m[i].length; j++) {
                if (max < Math.abs(m[i][j])) {
                    max = Math.abs(m[i][j]);
                }
            }
        }
        return max;
    }


}
