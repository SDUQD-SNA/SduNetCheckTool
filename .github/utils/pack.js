var fs = require('fs');
var child_process = require('child_process');
var generateEvb = require('generate-evb');

// Change the following paths to the actual paths used in your project
var evbCliPath = 'enigmavbconsole.exe';
var projectName = 'SDUNetCheckTool.evb';
var inputExe = '../../SduNetCheckTool.GUI/bin/Debug/SduNetCheckTool.GUI.exe';
var outputExe = '../../build/SduNetCheckTool.GUI_boxed.exe';
var path2Pack = '../../SduNetCheckTool.GUI/bin/Debug';

generateEvb(projectName, inputExe, outputExe, path2Pack);

child_process.execFile(evbCliPath, [projectName], function (err, stdout, stderr) {
    var success = false;
    if (!err) {
        // Sanity check (change this to what works for you):
        // Check if the output file exists and if it's bigger than the input file
        if (fs.existsSync(outputExe)) {
            success = fs.statSync(outputExe).size > fs.statSync(inputExe).size;
        }

        if (!success) {
            err = new Error('Failed to pack EVB project!\nEVB stdout:\n' + stdout + '\nEVB stderr:\n' + stderr);
        }
    }
    if (err) {
    	throw err;
    }
});
