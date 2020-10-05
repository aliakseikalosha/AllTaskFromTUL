package tul.alg1.lesson.tools;

public class DataTimeTools {


    public static  boolean isLeapYear(int year){
        /*if (year is not divisible by 4) then (it is a common year)
     else if (year is not divisible by 100) then (it is a leap year)
     else if (year is not divisible by 400) then (it is a common year)
     else (it is a leap year)
         */

        return (year%4 == 0) && (year % 100 !=0 || year % 400 == 0);
    }

    /**
     *
     * @param day
     * @param month
     * @param year
     * @return
     */
    public static  int DayOfWeek(int day, int month, int year){

        return 0;
    }
}
