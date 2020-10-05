function showTaylor(odx, step, dox,ody,doy, d,x0,func)
    x = odx:step:dox;
    for i=1:length(d)
        dd = d(1:i);
        y = Taylor(x,dd,x0);
        z = func(x);
        hold off
        plot(x,y)
        hold on
        plot(x,z)
        axis([odx,dox,ody,doy])
        pause(1)
    end
end