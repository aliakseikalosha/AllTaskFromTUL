package tul.alg1.lesson;

import java.util.Scanner;

public class Cviceni20191121 {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        //Max();
        //ReadToArray();
        //ReadFloatArray();
        CountNumbersinNumbers();
    }

    private static void CountNumbersinNumbers() {
        float[] count = new float[11];
        long c;
        System.out.print("Zadej cislo : ");
        while ((c = sc.nextLong()) > 0) {
            printArray("pocet cifet",CountNumber(c));
            System.out.print("Zadej cislo : ");
        }
    }
    private static int[] CountNumber(long c){
        int[] numbers = new int[10];
        long d = 10;
        while(c>0){
            numbers[(int)(c%d)]++;
            c/=d;
        }
        return numbers;
    }
    private static void ReadFloatArray() {
        float[] array = readArray(sc);
        printArray("Plole floatu ", array);
        array = new float[10000];
        int n = readArrayOfPositiveNumbers(array);
        printArray("Pole floatu s rozsahem " + n, array, n);
        printArray("Pole neznamecho rozsahu", readArrayOfPositiveNumbers());
    }

    private static void ReadToArray() {

        System.out.print("Zadej pocet hodnot : ");
        int n = sc.nextInt();
        int[] array = new int[n];
        for (int i = 0; i < n; i++) {
            System.out.print("Zadej hodnotu : ");
            array[i] = sc.nextInt();
        }
        printArray("for", array);
        n = 0;
        array = new int[n];
        int c;
        System.out.print("Zadej hodnotu : ");
        while ((c = sc.nextInt()) > 0) {
            n++;
            int[] b = new int[n];
            for (int i = 0; i < array.length; i++) {
                b[i] = array[i];
            }
            b[n - 1] = c;
            array = b;
            System.out.print("Zadej hodnotu : ");
        }
        printArray("while", array);
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

    private static float[] readArrayOfPositiveNumbers() {
        int maxSize = 10000;
        float[] v = new float[maxSize];
        int n = 0;
        float c;
        while ((c = sc.nextFloat()) > 0) {
            v[n++] = c;
        }
        float[] b = new float[n];
        for (int i = 0; i < n; i++) {
            b[i] = v[i];
        }
        return b;
    }

    private static int readArrayOfPositiveNumbers(float[] v) {
        int n = 0;
        float c;
        while ((c = sc.nextFloat()) > 0) {
            v[n++] = c;
        }
        return n;
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
        //for (int i = 0; i < a.length; i++) {
        //    System.out.print(a[i] + "\t");
        //}
        for (float i : a) {
            System.out.format("%f\t", i);
        }
        System.out.println();
    }

    private static void printArray(String title, int[] a) {
        System.out.println(title);
        //for (int i = 0; i < a.length; i++) {
        //    System.out.print(a[i] + "\t");
        //}
        for (int i : a) {
            System.out.format("%d\t", i);
        }
        System.out.println();
    }

    private static void Max() {
        int n = sc.nextInt();
        int max1, max2, max3;
        int count1, count2, count3;
        max1 = max2 = max3 = Integer.MIN_VALUE;
        count1 = count2 = count3 = 0;
        for (int i = 0; i < n; i++) {
            int c = sc.nextInt();
            if (max1 < c) {
                max3 = max2;
                max2 = max1;
                max1 = c;
                count3 = count2;
                count2 = count1;
                count1 = 0;
            } else if (max2 < c && max1 != c) {
                max3 = max2;
                max2 = c;
                count3 = count2;
                count2 = 0;
            } else if (max3 < c && max1 != c && max2 != c) {
                max3 = c;
                count3 = 0;
            }

            if (max1 == c) {
                count1++;
            } else if (max2 == c) {
                count2++;
            } else if (max3 == c) {
                count3++;
            }
        }

        System.out.println(max1 + " " + count1);
        System.out.println(max2 + " " + count2);
        System.out.println(max3 + " " + count3);
    }
}
