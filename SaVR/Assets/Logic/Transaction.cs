using System;
using System.Collections;
using System.Collections.Generic;

public class Transaction
{
	public enum TransactionStatus { Active, Shadow }
	public enum ApiSource { Finicity }

	float amount;
	long accountId;
	long customerId;
	long createdDate;
	String description;
	float escrowAmount;
	float feeAmount;
	long apiId;

	long institutionTransactionId;
	float interestAmount;
	long postedDate;
	float principalAmount;
	TransactionStatus status;
	
	public Transaction (float amount,
		long accountId,
		long customerId,
		long apiId,
		long institutionTransactionId,
		long createdDate,
		long postedDate,
		string description,
		float escrowAmount,
		float feeAmount,
		float interestAmount,
		float principalAmount,
		TransactionStatus status)
	{
		this.amount = amount;
		this.accountId = accountId;
		this.customerId = customerId;
		this.createdDate = createdDate;
		this.description = description;
		this.escrowAmount = escrowAmount;
		this.feeAmount = feeAmount;
		this.apiId = apiId;
		this.institutionTransactionId = institutionTransactionId;
		this.interestAmount = interestAmount;
		this.postedDate = postedDate;
		this.principalAmount = principalAmount;
		this.status = status;
	}

	public Transaction(float amount,
		long accountId,
		long customerId,
		long institutionTransactionId,
		long createdDate,
		long postedDate,
		string description){

		this.amount = amount;
		this.accountId = accountId;
		this.customerId = customerId;
		this.createdDate = createdDate;
		this.description = description;
		this.escrowAmount = 0.0f;
		this.feeAmount = 0.0f;
		this.apiId = 0;
		this.institutionTransactionId = institutionTransactionId;
		this.interestAmount = 0.0f;
		this.postedDate = postedDate;
		this.principalAmount = 0.0f;
		this.status = TransactionStatus.Active;

	}
}

