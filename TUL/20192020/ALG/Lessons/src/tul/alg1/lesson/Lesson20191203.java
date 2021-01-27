package tul.alg1.lesson;

import java.io.IOException;
import java.util.Scanner;

import static tul.alg1.lesson.tools.ArrayTools.printArray;

public class Lesson20191203 {

    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) throws IOException {
        readCharExsample();
        stringExsample();
    }

    private static void stringExsample() {
        System.out.println("Je to konstantni textovy retezec");
        String s1, s2, s3, s4, s5;
        char[] zn = {'C', 'a', 'u'};
        s1 = "Ahoj";
        s2 = new String("Ahoj");
        s3 = new String(zn);
        s4 = sc.nextLine();
        s5 = sc.next();
        String[] s = {"nultova", "prvni"};
        System.out.println(s1);
        System.out.println(s2);
        System.out.println(s3);
        System.out.println("next line");
        System.out.println(s4 + " delka je " + s4.length());
        System.out.println("next");
        System.out.println(s5);
        System.out.println(s);
        System.out.println("Je s1(" + s1 + ") stejny jako s2(" + s2 + ") ? odpoved  je " + s1.equals(s2));
        System.out.println("Obsahuji s1(" + s1 + ") stejnou reference s2(" + s2 + ") ? odpoved  je " + s1 == s2);
        System.out.println("Prirazime stejnou hodnotu");
        s1 = s2 = "Ahoj";
        System.out.println("Obsahuji s1(" + s1 + ") stejnou reference s2(" + s2 + ") ? odpoved  je " + s1 == s2);
        System.out.println("jake jsou vstahi mezi s4(" + s4 + ") a s5(" + s5 + ") kdyz odpoved je kladna s1 je vetsi, kdyz zaporna s2 je vetsi, jinak jsou stejny " + s1.compareTo(s2));
        printArray("Split \' \'", "Ahoj  lidi!".split(" "));
        printArray("Split \' +\'", "Ahoj  lidi!".split(" +"));
    }

    private static void readCharExsample() throws IOException {
        char ch = (char) System.in.read();
        System.out.println(ch);
        if (Character.isAlphabetic(ch)) {
            System.out.println("Je to z abcdy");
        } else if (Character.isDigit(ch)) {
            System.out.println("Je to cislo");
        }
        char[] chs = new char[]{'a', 'b', 'c', 'd', 'a'};
        System.out.println(chs);

    }
}
