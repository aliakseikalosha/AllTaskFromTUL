package tul.alg1.lesson;

import java.util.Scanner;

public class Lesson20191126 {
    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) throws IllegalAccessException {
        System.out.println("Prednaska : Pole ");
        /*
        {
            findMaximum();
            float[] a = {1f, 2f, 3f, 4f, 5f, 6f, 7f};

            printArray("Statycke inicializavane pole varinata 1 ", a);
            printArray("Statycke inicializavane pole varianta 2 ", new float[]{1, 2, 3, 4, 4, 5});
        }
        */

        {
            System.out.println("Dvourozmerne Pole ");
            int[][] a = new int[3][3];
            a[1][2] = 6;
            for (int i = a.length; i < 3; i++) {
                for (int j = 0; j < a[i].length; j++) {
                    a[i][j] = 0;
                }
            }
            float[][] matrix = readMetrix(sc);
            printMatrix("Nactena matice", matrix);
            printArray("Soucet radku : ", summOfLines(matrix));
            changeRow(matrix,0,1);
            printMatrix("Vymeneli 1 a 2 radky ", matrix);
            changeRow(matrix,0,1);
            chageColumns(matrix, 0,1);
            printMatrix("Vymenili 1 a 2 sloupce ", matrix);
            chageColumns(matrix, 0,1);
            printMatrix("Ukazka ruznerozmerne pole", new float[][]{{1,2,3,4},{1,2},{-1,2,3,4,55,6,4,341}});
        }
    }

    private static float[] summOfLines(float[][] m) {
        float[] sum = new float[m.length];
        for (int i = 0; i < m.length; i++) {
            sum[i] = 0;
            for (int j = 0; j < m[i].length; j++) {
                sum[i] += m[i][j];
            }
        }
        return sum;
    }

    private static float[][] readMetrix(Scanner s) {
        System.out.print("Zadej pocet radku : ");
        int n = sc.nextInt();
        System.out.print("Zadej pocet sloupcu : ");
        int m = sc.nextInt();
        float[][] matrix = new float[n][m];
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                System.out.format("Zadej prvek z %d-teho radku a %d-teho sloupce : ", i + 1, j + 1);
                matrix[i][j] = sc.nextFloat();
            }
        }
        return matrix;
    }

    private static void changeRow(float[][] m, int indexRow1, int indexRow2){
        float[] a = m[indexRow1];
        m[indexRow1] = m[indexRow2];
        m[indexRow2] = a;
    }

    private static void chageColumns(float[][] m, int indexColumn1,int indexColumn2){
        for (int i = 0; i < m.length; i++) {
                float a = m[i][indexColumn1];
                m[i][indexColumn1] = m[i][indexColumn2];
                m[i][indexColumn2] = a;
        }
    }

    private static void printMatrix(String title, float[][] m) {
        System.out.println(title);

        for (int i = 0; i < m.length; i++) {
            //System.out.print("| ");
            for (int j = 0; j < m[i].length; j++) {
                System.out.print(m[i][j] + " ");
            }
            System.out.println();
            //System.out.println("|");
        }
    }

    private static void findMaximum() throws IllegalAccessException {
        System.out.print("Zadej hodnoty : ");
        float[] a = readArray(sc);
        printArray("Vsechna cisla", a);
        System.out.print("Maximum : " + maximum(a));
    }

    private static int findIndexOfMaximum(float[] a) throws IllegalAccessException {
        if (a == null) {
            throw new IllegalAccessException("Neexistujece pole a : " + a);
        }
        if (a.length < 1) {
            return -1;
        }
        int index = 0;
        for (int i = 0; i < a.length; i++) {
            if (a[i] > a[index]) {
                index = i;
            }
        }
        return index;
    }

    private static boolean isSorted(float[] a) throws IllegalAccessException {
        throwErrorIfNullOrEmpty(a);
        return isSorted(a, a.length);
    }

    public static boolean isSorted(float[] a, int n) throws IllegalAccessException {
        throwErrorIfNullOrEmpty(a);
        for (int i = 1; i < n; i++) {
            if (a[i - 1] < a[i]) {
                return false;
            }
        }
        return true;
    }

    private static float maximum(float[] a) throws IllegalAccessException {
        throwErrorIfNullOrEmpty(a);
        return maximum(a, 0, a.length);
    }

    private static float maximum(float[] a, int n) throws IllegalAccessException {
        throwErrorIfNullOrEmpty(a);
        return maximum(a, 0, n);
    }

    private static float maximum(float[] a, int startIndex, int endIndex) throws IllegalAccessException {
        throwErrorIfNullOrEmpty(a);
        if (startIndex >= a.length || endIndex >= a.length) {
            return Float.NaN;
        }
        if (startIndex > endIndex) {
            throw new IllegalAccessException("startIndex > endIndex");
        }
        float max = Float.NEGATIVE_INFINITY;
        for (int i = startIndex; i < endIndex; i++) {
            if (max < a[i]) {
                max = a[i];
            }
        }
        return max;
    }

    private static float[] readArray(Scanner s) {
        int n = s.nextInt();
        float[] a = new float[n];
        for (int i = 0; i < n; i++) {
            System.out.print("Zadej hodnotu : ");
            a[i] = s.nextFloat();
        }

        return a;
    }

    private static void printArray(String title, float[] a, int n) {
        System.out.println(title);
        for (int i = 0; i < n; i++) {
            System.out.print(a[i] + "\t");
        }
        System.out.println();
    }

    private static void printArray(String title, float[] a) {
        System.out.println(title);
        for (float i : a) {
            System.out.format("%f\t", i);
        }
        System.out.println();
    }

    private static void printArray(String title, int[] a) {
        System.out.println(title);
        for (int i : a) {
            System.out.format("%d\t", i);
        }
        System.out.println();
    }

    private static void throwErrorIfNullOrEmpty(float[] a) throws IllegalAccessException {
        if (a == null) {
            throw new IllegalAccessException("Neexistujece pole a : " + a);
        }
        if (a.length < 1) {
            throw new IllegalAccessException("Pole je prazne a : " + a);
        }
    }
}
