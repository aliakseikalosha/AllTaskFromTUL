package tul.alg1.lesson;

import java.io.IOException;
import java.util.Scanner;

import static tul.alg1.lesson.tools.MatrixTools.*;

public class Cviceni20191213 {
    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) throws IOException {
        //changeMatrix();
        //diagonalMatrix();
        textExsample();
    }

    private static void textExsample(){
        System.out.println("Zadej textovy retezec");
        String  str = sc.nextLine();
        for (int i = 0; i < str.length(); i++) {
            char c = str.charAt(i);
            System.out.format(" %c",c);
        }

    }

    private static void diagonalMatrix() {
        int[][] m = readIntMatrix(sc);
        System.out.println(print(m));
        boolean isDiag = true;
        for (int i = 0; i < m.length; i++) {
            int c = Math.abs(m[i][i]);
            for (int x = 0; x < m.length; x++) {
                if (x == i) {
                    continue;
                }
                if (Math.abs(m[x][i]) > c || Math.abs(m[i][x]) > c) {
                    isDiag = false;
                    System.out.println(String.format("to ne plati pro x/y = %d i = %d ", x, i));
                }
            }
        }
        System.out.println((isDiag ? "Je" : "Neni") + " diagonalni dominantni");
    }

    private static void changeMatrix() {
        double[][] m = readMatrix(sc);
        double max = absMax(m);
        System.out.println(print(m));
        System.out.println("Max prvek " + max);
        for (int i = 0; i < m.length; i++) {
            for (int j = 0; j < m[i].length; j++) {
                m[i][j] /= max;
            }
        }
        System.out.println(print(m));
    }
}
