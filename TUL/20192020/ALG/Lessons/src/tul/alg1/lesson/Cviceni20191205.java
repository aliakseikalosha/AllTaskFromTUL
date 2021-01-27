package tul.alg1.lesson;

import java.io.IOException;
import java.util.Scanner;

import static tul.alg1.lesson.tools.MatrixTools.*;

public class Cviceni20191205 {
    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) throws IOException {
        //matrixExsample();
        //matrixCalculationExsample();
        matrixSym();
        matrixStoch();
        long a = 10;
        int b = 20;
        a =b;
        char c =33;

    }

    private static void matrixStoch(){
        double[][] m = readMatrix(sc);
        System.out.println("Zadana matice\n"+print(m));
        System.out.println("Matice "+(isStochastic(m, 1e-5)?"je":"neni")+" stochasticka matice");
    }

    private static boolean isStochastic(double[][] m, double eps) {

        for (int i = 0; i <m.length; i++) {
            double sum = 0;
            for (int j = 0; j < m[0].length; j++) {
                if(m[i][j]>0){
                    sum+=m[i][j];
                }else{
                    return false;
                }
            }
            if(Math.abs(1-sum)>eps){
                return false;
            }
        }
        return true;
    }

    private static void matrixSym() {
        int[][] m = readIntMatrix(sc);
        System.out.println("Zadana matice\n"+print(m));
        System.out.println("Matice "+(isSymetrical(m)? "je":"neni")+" symetritcka matice");
    }

    private static boolean isSymetrical(int[][] m) {

        if (m.length != m[0].length) {
            return false;
        }
        for (int i = 0; i < m.length; i++) {
            for (int j = 0; j < m[0].length; j++) {
                if(i == j ){
                    continue;
                }
                if(m[i][j] != m[j][i]){
                    return false;
                }
            }
        }
        return true;
    }

    private static void matrixCalculationExsample() {
        boolean isEnd = false;
        double[][] A = random(3, 3);
        double[][] B = random(3, 3);
        do {
            drawMenuCE();
            int choise = sc.nextInt();
            switch (choise) {
                case 0:
                    isEnd = true;
                    break;
                case 1:
                    A = readMatrix(sc);
                    break;
                case 2:
                    B = readMatrix(sc);
                    break;
                case 3:
                    printMatrixCE(A, B);
                    break;
                case 4:
                    printAddMatrixCE(A, B);
                    break;
                case 5:
                    printMultMatrixCE(A, B);
                    break;
                default:
                    System.out.println("Zadana volba je chibna");
            }
        } while (!isEnd);
    }

    private static void printAddMatrixCE(double[][] a, double[][] b) {
        if (a == null || b == null) {
            System.out.println("aspon jedna matice neni zadana ");
            return;
        }
        System.out.println("Soucet matic \n" + print(add(a, b)));
    }

    private static void printMultMatrixCE(double[][] a, double[][] b) {
        if (a == null || b == null) {
            System.out.println("aspon jedna matice neni zadana ");
            return;
        }
        System.out.println("Soucin matic \n" + print(mult(a, b)));
    }

    private static void printMatrixCE(double[][] a, double[][] b) {
        if (a == null || b == null) {
            System.out.println("aspon jedna matice neni zadana ");
            return;
        }
        System.out.println("Matice A\n" + print(a) + "Matice B\n" + print(b));
    }

    private static void drawMenuCE() {
        System.out.println();
        System.out.println("======Menu======");
        System.out.println("1. nacti A");
        System.out.println("2. nacti B");
        System.out.println("3. vypis A a B");
        System.out.println("4. vypocti A+B");
        System.out.println("5. vypocti A*B");
        System.out.println("0. ukonci");
    }

    private static void matrixExsample() {
        int[][] mat = new int[5][6];
        for (int i = 0; i < mat.length; i++) {
            for (int j = 0; j < mat[i].length; j++) {
                mat[i][j] = (int) (100 * Math.random() + 1);
            }
        }
        for (int i = 0; i < mat.length; i++) {
            for (int j = 0; j < mat[i].length; j++) {
                System.out.format(" %3d", mat[i][j]);
            }
            System.out.println();
        }
    }
}
