if (localStorage['keybt0hutbo'] == 'true') {
	document.body.setAttribute('style',
		'font-size: 35px !important;' +
		''
	);
	
	let elems = document.querySelectorAll("div#nsbanner");
	for (elem of elems) {
		elem.setAttribute('style',
			'height: 60px;' +
			''
		);
	}
}
else {
	document.body.setAttribute('style',
		'font-size: 16px !important' +
		''
	);
}

// ================================
function divrightClick(e) {
	if (e.target.nodeName == 'A') {
		localStorage["destination"] = '' + e.target;
	}
}

document.addEventListener('DOMContentLoaded', function (event) {
	localStorage["destination"] = '' + document.location;
});
// ================================
