const websocket = require('ws');

const wss = new websocket.Server({port : 8080} , ()=>{
	console.log('started');
});

wss.on('connection', function connection(ws) {
	ws.on('message', (data) => {
		console.log('data received %o' , data.toString());
		var lune = require('lune');
	    
		var date = new Date(data.toString());
		
        var current_phase = lune.phase(date);
        var age = current_phase['age'];
		
		if(age < 1.8566)
			ws.send('New');
		else if(age < 5.53699)
			ws.send('Waxing Crescent');
		else if(age < 9.22831)
			ws.send('First Quarter');
		else if(age < 12.91963)
			ws.send('Waxing Gibbous');
		else if(age < 16.61096)
			ws.send('Full');
		else if(age < 20.30228)
			ws.send('Waning Gibbous');
		else if(age < 23.99361)
			ws.send('Last Quarter');
		else if(age < 27.68493)
		    ws.send('Waning Crescent');
		
    });
});

wss.on('listening', ()=>{
	console.log('listening on 8080')
});