package tul.alg1.lesson.vanocniuloha;

import java.util.Random;
import java.util.Scanner;

public class Kalosha {
    private boolean hasTree;
    private int minPresentSize;
    private int maxPresentSize;
    private static Scanner sc = new Scanner(System.in);
    private static Random rnd = new Random();

    public static void main(String[] args) {
        new Kalosha().Draw();
    }

    public void Draw(){
        System.out.print("Zadej maximalni velikost obrazku XxX, X = ");
        maxPresentSize = sc.nextInt();
        System.out.print("Zadej minimalni velikost obrazku XxX (a mensi nez "+ maxPresentSize+") X = ");
        minPresentSize = sc.nextInt();
        int size = minPresentSize + rnd.nextInt(maxPresentSize - minPresentSize);


    }
}
