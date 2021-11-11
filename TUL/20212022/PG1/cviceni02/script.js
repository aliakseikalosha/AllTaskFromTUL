
modeIndex = 0;
const functionCount = 3;
const dic = {
	2:convertGreyLevels,
	1:convertMatrix,
	0:convertMatrixWithErrorDistribution,
};

// Callback function called, when file is "opened"
function handleFileSelect(item) {
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
					var srcCanvas = document.getElementById("src");
					var srcContext = srcCanvas.getContext("2d");

					// Change size of canvas
					srcCanvas.height = srcImg.height;
					srcCanvas.width = srcImg.width;

					srcContext.drawImage(srcImg, 0, 0);

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
	}
}

data = null;
// Callback function called, when clicked at Convert button
function convertImage() {
	var srcCanvas = document.getElementById("src");
	var srcContext = srcCanvas.getContext("2d");
	var canvasHeight = srcCanvas.height;
	var canvasWidth = srcCanvas.width;

	if (data == null){
		var srcImageData = srcContext.getImageData(0, 0, canvasWidth, canvasHeight);
		data = copyImageData(srcContext, srcImageData);		
	}

	var newImage = copyImageData(srcContext, data) 
	var f = dic[modeIndex];
	f(newImage);
	modeIndex = (modeIndex + 1) % functionCount
	srcContext.putImageData(newImage, 0, 0);
}

function copyImageData(ctx, src)
{
    var dst = ctx.createImageData(src.width, src.height);
    dst.data.set(src.data);
    return dst;
}

function convertGreyLevels(imgData) {
	var rawData = imgData.data;

	// Go through the image using x,y coordinates
	var pixelIndex, red, green, blue, alpha;
	for(var y = 0; y < imgData.height; y++) {
		for(var x = 0; x < imgData.width; x++) {
			pixelIndex = ( (imgData.width * y) + x) * 4
			red   = rawData[pixelIndex + 0];
			green = rawData[pixelIndex + 1];
			blue  = rawData[pixelIndex + 2];
			alpha = rawData[pixelIndex + 3];

			// Do magic at this place :-)
			grey = 0.299 * red + 0.587 * green + 0.114 * blue;
			rawData[pixelIndex + 0] = grey;
			rawData[pixelIndex + 1] = grey;
			rawData[pixelIndex + 2] = grey;
			rawData[pixelIndex + 3] = alpha;
		}
	}	
}

function convertMatrix(imgData) {
	var rawData = imgData.data;
	n = m = 4
	Matrix = [
		0,  12, 3,  15,
		8,  4,  11, 7,
		2,  14, 1,  13,
		10, 6,  9,  5,
	]
	getM = (x,y)=> Matrix[n*(y % m)+(x % n)]
	// Go through the image using x,y coordinates
	var pixelIndex, red, green, blue, alpha;
	for(var y = 0; y < imgData.height; y++) {
		for(var x = 0; x < imgData.width; x++) {
			pixelIndex = ( (imgData.width * y) + x) * 4
			red   = rawData[pixelIndex + 0];
			green = rawData[pixelIndex + 1];
			blue  = rawData[pixelIndex + 2];
			alpha = rawData[pixelIndex + 3];

			grey_in = 0.299 * red + 0.587 * green + 0.114 * blue;
			k = 15;
			grey_out = 0;
			// Do magic at this place :-)
			M = getM(x,y);
			if(grey_in > k*M){
				grey_out = 255;
			}
			else{
				grey_out = 0;
			}
			grey = grey_out;
			rawData[pixelIndex + 0] = grey;
			rawData[pixelIndex + 1] = grey;
			rawData[pixelIndex + 2] = grey;
			rawData[pixelIndex + 3] = alpha;
		}
	}	
}

function convertMatrixWithErrorDistribution(imgData) {

	console.log("Start convertMatrixWithErrorDistribution")	
	var rawData = imgData.data;
	// Go through the image using x,y coordinates
	var pixelIndex, red, green, blue, alpha;
	getPixelIndex = (x,y) => ( (imgData.width * y) + x) * 4
	setPixel = (x, y, grey, a) => {
		i = getPixelIndex(x,y)
		rawData[i + 0] = grey;
		rawData[i + 1] = grey;
		rawData[i + 2] = grey;
		rawData[i + 3] = a;
	} 
	getPixelGrey =  function(x,y) {
		i = getPixelIndex(x,y);
		return rawData[i + 0]
	}
	n = m = 4
	Matrix = [
		0,  12, 3,  15,
		8,  4,  11, 7,
		2,  14, 1,  13,
		10, 6,  9,  5,
	]
	getM = (x,y)=> Matrix[n*(y % m)+(x % n)]
	for(var y = 0; y < imgData.height; y++) {
		for(var x = 0; x < imgData.width; x++) {
			pixelIndex = getPixelIndex(x,y);
			red   = rawData[pixelIndex + 0];
			green = rawData[pixelIndex + 1];
			blue  = rawData[pixelIndex + 2];
			alpha = rawData[pixelIndex + 3];

			grey = 0.299 * red + 0.587 * green + 0.114 * blue;
			setPixel(x,y,grey,alpha);
		}
	}
	for(var y = 0; y < imgData.height; y++) {
		for(var x = 0; x < imgData.width; x++) {
			grey_in = getPixelGrey(x,y);
			k = 15;
			grey_out = 0;
			M = getM(x,y);
			if(grey_in > k*M){
				grey_out = 255;
			}
			else{
				grey_out = 0;
			}
			grey = grey_out;
			setPixel(x,y,grey_out,alpha);
			error = grey_out - grey_in;
			setPixel(x+1,y, getPixelGrey(x+1,y) + (7/16)*error, 255);
			setPixel(x-1,y+1, getPixelGrey(x-1,y+1) + (3/16)*error, 255);
			setPixel(x,y+1, getPixelGrey(x,y+1) + (5/16)*error, 255);
			setPixel(x+1,y+1, getPixelGrey(x+1,y+1) + (1/16)*error, 255);
		}
	}
	console.log("Done!")	
}