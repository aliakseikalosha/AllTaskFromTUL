// Callback function called, when file is "opened"
function handleFileSelect(item, elementName) {
    var files = item.files;

    console.log(files);

    for (var i = 0; i < files.length; i++) {
        console.log(files[i], files[i].name, files[i].size, files[i].type);

        // Only process image files.
        if (!files[i].type.match('image.*')) {
            continue;
        }

        var reader = new FileReader();

        // Closure for loading image to memory
        reader.onload = (function(file) {
            return function(evt) {
                var srcImg = new Image();
                srcImg.src = evt.target.result;

                srcImg.onload = function() {
                    var srcCanvas = document.getElementById(elementName);
                    var srcContext = srcCanvas.getContext("2d");

                    // Change size of canvas
                    srcCanvas.height = srcImg.height;
                    srcCanvas.width = srcImg.width;

                    srcContext.drawImage(srcImg, 0, 0);

                    var dstCanvas = document.getElementById("result");
                    dstCanvas.height = srcImg.height;
                    dstCanvas.width = srcImg.width;

                    var convertButton = document.getElementById("convert");
                    // Enabled button
                    convertButton.disabled = false;
                    // Add callback
                    convertButton.addEventListener('click', convertImage, false);
                }
            }
        })(files[i]);

        reader.readAsDataURL(files[i]);

        break;
    };
};


// Callback function called, when clicked at Convert button
function convertImage() {
    var personCanvas = document.getElementById("person");
    var personContext = personCanvas.getContext("2d");
    var canvasHeight = personCanvas.height;
    var canvasWidth = personCanvas.width;

    var personImageData = personContext.getImageData(0, 0, canvasWidth, canvasHeight);
    var backgroundImageData = document.getElementById("background").getContext("2d").getImageData(0, 0, canvasWidth, canvasHeight);
    var logoImageData = document.getElementById("logo").getContext("2d").getImageData(0, 0, canvasWidth, canvasHeight);
    var resultImageData = document.getElementById("result").getContext("2d").getImageData(0, 0, canvasWidth, canvasHeight);

    convertImageData(personImageData, backgroundImageData, logoImageData, resultImageData);

    document.getElementById("result").getContext("2d").putImageData(resultImageData, 0, 0);
};

// Function for converting raw data of image
function convertImageData(personImageData, backgroundImageData, logoImageData, resultImageData) {
    var personData = personImageData.data;
    var backgroundData = backgroundImageData.data;
    var logoData = logoImageData.data;
    var resultData = resultImageData.data;
    var color = hexToRgb(document.getElementById("backgroundColor").value);
    var hTol = document.getElementById("H").valueAsNumber;
    var sTol = document.getElementById("S").valueAsNumber;
    var lTol = document.getElementById("L").valueAsNumber;
    // Go through the image using x,y coordinates
    var red, green, blue, alpha, personP, logoP;

    getPesonPixel = function(i){
        var r = personData[i + 0];
        var g = personData[i + 1];
        var b = personData[i + 2];
        var a = 0;
        [h,s,l] = rgbToHsl(r,g,b);
        [ch,cs,cl] = rgbToHsl(color[0],color[1],color[2])
        
        if(Math.abs(h-ch) > hTol || Math.abs(s-cs) > sTol || Math.abs(l-cl) > lTol){
            a = 1;
        }

        return [r,g,b,a];
    }

    getLogoPixel = function(i){
        logoImageData.width
        var r = logoData[i + 0];
        var g = logoData[i + 1];
        var b = logoData[i + 2];
        var a = logoData[i + 4];
        
        grey = 0.299 * r + 0.587 * g + 0.114 * b;

        return [grey, a];
    }

    blendResult = function(c0, c1, a){
        a = a / 255.0;
        return c0 * ( 1 - a) + c1 * a;
    }
	getPixelIndex = (x,y,imgData) => ( (imgData.width * y) + x) * 4
    for (var pixelIndex = 0; pixelIndex < personData.length; pixelIndex += 4) {
        red = backgroundData[pixelIndex + 0];
        green = backgroundData[pixelIndex + 1];
        blue = backgroundData[pixelIndex + 2];
        alpha = backgroundData[pixelIndex + 3];
        

        // Do magic at this place
        //console.log(red, green, blue, alpha);
        personP = getPesonPixel(pixelIndex);
        if(personP[3]>0){
            [red, green,blue ,_] = personP;
        }
        resultData[pixelIndex + 0] = red;
        resultData[pixelIndex + 1] = green;
        resultData[pixelIndex + 2] = blue;
        resultData[pixelIndex + 3] = 255;
    }

    var deltaX = backgroundImageData.width - logoImageData.width; 
    for (let x = 0; x < logoImageData.width; x++) {
        for (let y = 0; y < logoImageData.height; y++) {
            pixelIndex = getPixelIndex(deltaX + x,y,backgroundImageData);
            red = resultData[pixelIndex + 0];
            green = resultData[pixelIndex + 1];
            blue = resultData[pixelIndex + 2];
            alpha = resultData[pixelIndex + 3];
            
            logoP = getLogoPixel(getPixelIndex(x,y,logoImageData));
            resultData[pixelIndex + 0] = blendResult(red, logoP[0], logoP[1])
            resultData[pixelIndex + 1] = blendResult(green, logoP[0], logoP[1]);
            resultData[pixelIndex + 2] = blendResult(blue, logoP[0], logoP[1]);
            resultData[pixelIndex + 3] = 255;
        }
        
    }
}

function hexToRgb(hex) {
    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    if(result){
        var r= parseInt(result[1], 16);
        var g= parseInt(result[2], 16);
        var b= parseInt(result[3], 16);
        return  [r,g,b];
    } 
    return null;
}


//https://gist.github.com/mjackson/5311256
/**
 * Converts an RGB color value to HSL. Conversion formula
 * adapted from http://en.wikipedia.org/wiki/HSL_color_space.
 * Assumes r, g, and b are contained in the set [0, 255] and
 * returns h, s, and l in the set [0, 1].
 *
 * @param   Number  r       The red color value
 * @param   Number  g       The green color value
 * @param   Number  b       The blue color value
 * @return  Array           The HSL representation
 */
 function rgbToHsl(r, g, b) {
    r /= 255, g /= 255, b /= 255;
  
    var max = Math.max(r, g, b), min = Math.min(r, g, b);
    var h, s, l = (max + min) / 2;
  
    if (max == min) {
      h = s = 0; // achromatic
    } else {
      var d = max - min;
      s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
  
      switch (max) {
        case r: h = (g - b) / d + (g < b ? 6 : 0); break;
        case g: h = (b - r) / d + 2; break;
        case b: h = (r - g) / d + 4; break;
      }
  
      h /= 6;
    }
  
    return [ h, s, l ];
  }
  
  /**
   * Converts an HSL color value to RGB. Conversion formula
   * adapted from http://en.wikipedia.org/wiki/HSL_color_space.
   * Assumes h, s, and l are contained in the set [0, 1] and
   * returns r, g, and b in the set [0, 255].
   *
   * @param   Number  h       The hue
   * @param   Number  s       The saturation
   * @param   Number  l       The lightness
   * @return  Array           The RGB representation
   */
  function hslToRgb(h, s, l) {
    var r, g, b;
  
    if (s == 0) {
      r = g = b = l; // achromatic
    } else {
      function hue2rgb(p, q, t) {
        if (t < 0) t += 1;
        if (t > 1) t -= 1;
        if (t < 1/6) return p + (q - p) * 6 * t;
        if (t < 1/2) return q;
        if (t < 2/3) return p + (q - p) * (2/3 - t) * 6;
        return p;
      }
  
      var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
      var p = 2 * l - q;
  
      r = hue2rgb(p, q, h + 1/3);
      g = hue2rgb(p, q, h);
      b = hue2rgb(p, q, h - 1/3);
    }
  
    return [ r * 255, g * 255, b * 255 ];
  }