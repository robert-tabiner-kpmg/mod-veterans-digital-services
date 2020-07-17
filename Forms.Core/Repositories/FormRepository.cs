using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.ExternalFrameworks;
using Forms.Core.Models.InFlight;
using Forms.Core.Repositories.Interfaces;
using Graph.Models;
using Microsoft.AspNetCore.Http;

namespace Forms.Core.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ICacheFramework _cacheFramework;
        
        /*
         * We hold a dictionary of the forms requested in the instance of the FormRepository
         * This is intended to be used as a Scoped service so these are disposed after each http request
         */
        private readonly IDictionary<string, Graph<Key, FormNode>> _forms;

        public FormRepository(ICacheFramework cacheFramework)
        {
            _cacheFramework = cacheFramework;
            _forms = new Dictionary<string, Graph<Key, FormNode>>();
        }

        public async Task<Graph<Key, FormNode>> GetForm(string formKey)
        {
            // Check to see if we have already accessed the graph in the scope of this http request
            if (_forms.TryGetValue(formKey, out var form))
                return form;

            form = await _cacheFramework.Get<Graph<Key, FormNode>>(formKey);
            
            // Build the neighbors collection
            foreach (var node in form.Nodes)
            {
                foreach (var neighborId in node.NeighborIds)
                {
                    node.Neighbors.Add(form.Nodes.FindByKey(neighborId));
                }
            }
            
            // Save the graph in case it is requested later in the http request
            _forms.Add(formKey, form);

            return form;
        }

        public async Task SaveForm(string formKey, Graph<Key, FormNode> form)
        {
            await _cacheFramework.Save(formKey, form);
            
            // Update the scoped copy of the form
            _forms[formKey] = form;
        }
    }
}