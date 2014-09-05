<?php include('shared/_header.php'); ?>

<h1>Forms</h1>
<p>Forms are relatively straight-forward, with Selenium doing most of the work.</p>

<h4>Simple form</h4>
<script type="text/javascript">
$(function () {

	var SimpleFormVM = function () {
		var self = this;
		
		self.email = ko.observable();
		self.name = ko.observable();
		self.users = ko.observableArray([
			{
				email: 'marcoo@infosupport.com',
				name: 'Marco vd Oever'
			}
		]);
		
		self.onSubmitSimpleForm = function (form) {
			self.users.push({ email: self.email(), name: self.name() });
			self.email('');
			self.name('');
		};
	};

	var simpleVM = new SimpleFormVM();
	ko.applyBindings(simpleVM, $('#simple-form')[0]);
	
});
</script>
<div id="simple-form">
	<form role="form" data-bind="submit: onSubmitSimpleForm">
	  <div class="form-group">
		<label for="simple-email">Email address</label>
		<input type="email" class="form-control" id="simple-email" placeholder="Enter email" data-bind="value: email">
	  </div>
	  <div class="form-group">
		<label for="simple-name">Name</label>
		<input type="text" class="form-control" id="simple-name" placeholder="Name" data-bind="value: name">
	  </div>
	  <button id="btn-simple-form-submit" type="submit" class="btn btn-default">Submit</button>
	</form>
	
	<table class="table">
		<thead>
			<tr>
				<th>Email</th>
				<th>Name</th>
			</tr>
		</thead>
		<tbody data-bind="foreach: users">
			<tr>
				<td data-bind="text: email"></td>
				<td data-bind="text: name"></td>
			</tr>
		</tbody>
	</table>
</div>

<?php include('shared/_footer.php'); ?>