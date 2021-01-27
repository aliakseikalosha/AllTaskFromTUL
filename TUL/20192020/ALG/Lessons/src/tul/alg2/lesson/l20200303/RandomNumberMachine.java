package tul.alg2.lesson.l20200303;


import java.util.Random;

public class RandomNumberMachine {
    private int maxNumber;
    private int minNunber;
    private Number[] numbers;
    private  static Random rnd = new Random();

    public RandomNumberMachine(int min, int max, int count) {
        numbers = new Number[count];
        maxNumber = max;
        minNunber = min;
        for (int i = 0; i < count; i++) {

            numbers[i] = new Number(minNunber+rnd.nextInt(maxNumber-minNunber),false);
        }
    }

    private class Number {
        public int n = -1;
        public boolean unused = true;

        public Number(int i, boolean b) {
            n=i;
            unused=b;
        }
    }
}
