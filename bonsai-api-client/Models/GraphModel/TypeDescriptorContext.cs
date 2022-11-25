﻿using System;
using System.ComponentModel;

namespace bonsai_api_client.Models.GraphModel
{
    class TypeDescriptorContext : ITypeDescriptorContext
    {
        readonly IServiceProvider provider;

        public TypeDescriptorContext(object instance, PropertyDescriptor propertyDescriptor)
            : this(instance, propertyDescriptor, null)
        {
        }

        public TypeDescriptorContext(object instance, PropertyDescriptor propertyDescriptor, IServiceProvider serviceProvider)
        {
            Instance = instance;
            PropertyDescriptor = propertyDescriptor;
            provider = serviceProvider;
        }

        public virtual System.ComponentModel.IContainer Container
        {
            get { return null; }
        }

        public object Instance { get; private set; }

        public virtual void OnComponentChanged()
        {
        }

        public virtual bool OnComponentChanging()
        {
            return true;
        }

        public PropertyDescriptor PropertyDescriptor { get; private set; }

        public virtual object GetService(Type serviceType)
        {
            if (provider != null)
            {
                return provider.GetService(serviceType);
            }

            return null;
        }
    }
}
