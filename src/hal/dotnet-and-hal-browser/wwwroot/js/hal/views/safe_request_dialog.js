HAL.customPostForm = Backbone.View.extend({
	initialize: function (opts) {
		this.href = opts.href.split('{')[0];
		this.method = opts.method;
		this.vent = opts.vent;
		_.bindAll(this, 'createNewResource');
	},

	events: {
		'submit form': 'createNewResource'
	},

	className: 'modal fade',

	createNewResource: function (e) {
		e.preventDefault();

		var self = this;

		var data = {}
		Object.keys(this.schema.properties).forEach(function(property) {
			if (!("format" in self.schema.properties[property])) {
				data[property] = self.$('input[name=' + property + ']').val();
			}
		});

		var opts = {
			url: this.$('.url').val(),
			headers: HAL.parseHeaders(this.$('.headers').val()),
			method: this.$('.method').val(),
			data: JSON.stringify(data)
		};

		var request = HAL.client.request(opts);
		request.done(function (response) {
			self.vent.trigger('response', {resource: response, jqxhr: jqxhr});
		}).fail(function (response) {
			self.vent.trigger('fail-response', {jqxhr: jqxhr});
		}).always(function () {
			self.vent.trigger('response-headers', {jqxhr: jqxhr});
			window.location.hash = 'NON-GET:' + opts.url;
		});

		this.$el.modal('hide');
	},

	render: function (opts) {
		var headers = HAL.client.getHeaders();
		var headersString = '';

		_.each(headers, function (value, name) {
			headersString += name + ': ' + value + '\n';
		});

		var request = HAL.client.request({
			url: this.href + '/schema',
			method: 'GET'
		});

		var self = this;
		request.done(function (schema) {
			self.schema = schema;
			self.$el.html(self.template({
				href: self.href,
				method: self.method,
			    schema: self.schema,
			    user_defined_headers: headersString}));
			self.$el.modal();
		});

		return this;
	},
	template: _.template($('#dynamic-request-template').html())
});
