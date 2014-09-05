<?php include('shared/_header.php'); ?>

<h1>Tables</h1>
<p>Tables are somewhat tricky to select, since they can contain multiple rows. The trick is to have one column, or a set of columns, that uniquely identify the row.</p>

<h4>Tables</h4>
<table id="products" class="table">
  <tr>
	<th style="width: 40px;">Id</th>
	<th>Name</th>
	<th style="width: 100px;">Price</th>
	<th style="width: 100px;">In stock</th>
  </tr>
  <tr>
	<td>1</td>
	<td>Space scooter</td>
	<td>40,00</td>
	<td>14</td>
  </tr>
  <tr>
	<td>2</td>
	<td>Lego technic truck</td>
	<td>55,00</td>
	<td>23</td>
  </tr>
  <tr>
	<td>3</td>
	<td>Cars puzzel</td>
	<td>10,99</td>
	<td>12</td>
  </tr>
  <tr>
	<td>4</td>
	<td>Pokemon X 3DS</td>
	<td>49,99</td>
	<td>17</td>
  </tr>
</table>

<h4>Empty table</h4>
<table id="empty-table" class="table">
  <thead>
	<tr>
	  <th style="width: 40px;">Id</th>
	  <th>Name</th>
	  <th style="width: 100px;">Price</th>
	  <th style="width: 100px;">In stock</th>
	</tr>
  </thead>
</table>

<h4>One element</h4>
<table id="one-element" class="table">
  <thead>
	<tr>
	  <th style="width: 40px;">Id</th>
	  <th>Name</th>
	  <th style="width: 100px;">Price</th>
	  <th style="width: 100px;">In stock</th>
	</tr> 
  </thead>
  <tbody>
	<tr>
	  <td>40</td>
	  <td>First element</td>
	  <td>100,20</td>
	  <td>Yes</td>
	</tr>
  </tbody>
</table>

<?php include('shared/_footer.php'); ?>