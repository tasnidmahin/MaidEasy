const form = document.getElementById('form');
const username = document.getElementById('username');
const name = document.getElementById('name');
const phone = document.getElementById('Phoneno');
const presentaddress = document.getElementById('presentaddress');
const permanentaddress = document.getElementById('permanentaddress');
const oldpassword = document.getElementById('oldpassword');
const password = document.getElementById('password');
const password2 = document.getElementById('password2');
const thana = document.getElementById('thana');
const submit = document.getElementById('submit-btn');
/*
form.addEventListener('submit', e => {
	e.preventDefault();

	checkInputs();
	
});
*/
submit.addEventListener('click', e => {
	e.preventDefault();

	checkInputs();


});

function checkInputs() {
	// trim to remove the whitespaces
	const usernameValue = username.value.trim();
	const nameValue = name.value.trim();

	const phoneValue = phone.value.trim();
	const presentaddressValue = presentaddress.value.trim();
	const permanentaddressValue = permanentaddress.value.trim();
	const oldpasswordValue = oldpassword.value.trim();
	const passwordValue = password.value.trim();
	const password2Value = password2.value.trim();
	const thanaValue = thana.value.trim();

	if (usernameValue === '') {
		setErrorFor(username, 'Username cannot be blank');
		return;
	} else {
		setSuccessFor(username);
	}
	if (nameValue === '') {
		setErrorFor(name, 'Name cannot be blank');
		return;
	} else {
		setSuccessFor(name);
	}
	if (phoneValue === '') {
		setErrorFor(phone, 'Phone no. cannot be blank');
		return;
	} else if (!isPhone(phoneValue)) {
		setErrorFor(phone, 'Not a valid Phone No.');
		return;
	} else {
		setSuccessFor(phone);
	}

	if (presentaddressValue === '') {
		setErrorFor(presentaddress, 'Present address cannot be blank');
		return;
	} else {
		setSuccessFor(presentaddress);
	}

	if (thanaValue === '') {
		setErrorFor(thana, 'Thana cannot be blank');
		return;
	}
	else if (!thanalist.includes(thanaValue)) {

		setErrorFor(thana, 'Thana did not match');
		return;


	}
	else {
		setSuccessFor(thana);
	}

	if (permanentaddressValue === '') {
		setErrorFor(permanentaddress, 'Permanent address cannot be blank');
		return;
	} else {
		setSuccessFor(permanentaddress);
	}
	/*if(emailValue === '') {
		setErrorFor(email, 'Email cannot be blank');
	} else if (!isEmail(emailValue)) {
		setErrorFor(email, 'Not a valid email');
	} else {
		setSuccessFor(email);
	} */

	if (oldpasswordValue === '') {
		setErrorFor(oldpassword, 'Password cannot be blank');
		return;
	} else {
		setSuccessFor(oldpassword);
	}
	if (passwordValue === '') {
		setErrorFor(password, 'Password cannot be blank');
		return;
	} else {
		setSuccessFor(password);
	}

	if (password2Value === '') {
		setErrorFor(password2, 'Password2 cannot be blank');
		return;
	} else if (passwordValue !== password2Value) {
		setErrorFor(password2, 'Passwords does not match');
		return;
	} else {
		setSuccessFor(password2);
	}
	form.submit();

}

function setErrorFor(input, message) {
	const formControl = input.parentElement;
	const small = formControl.querySelector('small');
	formControl.className = 'form-control error';
	small.innerText = message;
}

function setSuccessFor(input) {
	const formControl = input.parentElement;
	formControl.className = 'form-control success';
}

function isPhone(phone) {
	/* return /01d{ 9 }/.test(phone); */
	var phoneno = /^\+88\d{11}$/;
	if (phone.match(phoneno)) {
		return true;
	}
	else {

		return false;
	}
}

/*function isEmail(email) {
	return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
}
  */












