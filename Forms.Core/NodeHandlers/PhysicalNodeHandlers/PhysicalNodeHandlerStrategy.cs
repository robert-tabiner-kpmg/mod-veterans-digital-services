using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Extensions;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Interfaces;
using Forms.Core.NodeHandlers.PhysicalNodeHandlers.Models;
using Graph;
using Graph.Models;

namespace Forms.Core.NodeHandlers.PhysicalNodeHandlers
{
    public class PhysicalNodeHandlerStrategy : IPhysicalNodeHandlerStrategy
    {
        private readonly IEnumerable<IPhysicalNodeHandler> _nodeHandlers;

        public PhysicalNodeHandlerStrategy(IEnumerable<IPhysicalNodeHandler> nodeHandlers)
        {
            _nodeHandlers = nodeHandlers;
        }

        public Task<PhysicalResponse> Handle(FormType formType, string formKey, GraphNode<Key, FormNode> node)
        {
            var physicalFormNode =  node.AssertType<PhysicalFormNode>();
            
            var handler = _nodeHandlers.FirstOrDefault(x => x.PhysicalFormNodeType == physicalFormNode.PhysicalFormNodeType);

            if (handler is null)
                throw new NotImplementedException(physicalFormNode.PhysicalFormNodeType.ToString());

            return handler.Handle(formType, formKey, node);
        }
    }
}